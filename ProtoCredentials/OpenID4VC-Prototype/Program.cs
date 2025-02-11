using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpenID4VC_Prototype.Application.Interfaces;
using OpenID4VC_Prototype.Application.Services;
using OpenID4VC_Prototype.Domain.Interfaces;
using OpenID4VC_Prototype.Domain.Services;
using OpenID4VC_Prototype.Infrastructure.Configurations;
using OpenID4VC_Prototype.Presentation.Console;
using Serilog;
using didConfig = OpenID4VC_Prototype.Infrastructure.Configurations.DIdConfig;

var builder = Host.CreateDefaultBuilder(args)
    .UseSerilog((hostContext, loggerConfiguration) =>
    {
        loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<didConfig>(hostContext.Configuration.GetSection("DIdConfiguration"));
        services.AddSingleton(sp =>
        {
            var appConfig = sp.GetRequiredService<IOptions<didConfig>>().Value;
            return DIdConfigMapper.ToDomainModel(appConfig);
        });

        services.AddSingleton<ICryptoService, CryptoService>();
        services.AddScoped<IIssuerService, IssuerService>();
        services.AddScoped<IVerifierService, VerifierService>();
        services.AddSingleton<DIdService>();
    }).Build();

using var scope = builder.Services.CreateScope();

var issuerService = scope.ServiceProvider.GetRequiredService<IIssuerService>();
var verifierService = scope.ServiceProvider.GetRequiredService<IVerifierService>();
var dIdService = scope.ServiceProvider.GetRequiredService<DIdService>();

var issuer = dIdService.GenerateDId();
var holder = dIdService.GenerateDId();
var verifier = dIdService.GenerateDId();

Console.WriteLine($"Issuer DID: {issuer.DId}");
Console.WriteLine($"Holder DID: {holder.DId}");
Console.WriteLine($"Verifier DID: {verifier.DId}");

Presentation.PresentVCFlow(issuer, issuerService, holder, verifierService);

Log.CloseAndFlush();
