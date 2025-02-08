using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenID4VC_Prototype.Application.Interfaces;
using OpenID4VC_Prototype.Application.Services;
using OpenID4VC_Prototype.Domain.Interfaces;
using OpenID4VC_Prototype.Domain.Services;
using OpenID4VC_Prototype.Infrastructure.Configurations;

var builder = Host.CreateDefaultBuilder(args)
    .UseSerilog((hostContext, loggerConfiguration) =>
    {
        loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<DIdConfiguration>(hostContext.Configuration.GetSection("DIdConfiguration"));

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

// Issuing a verifiable credential
WriteTitle("Issuing verifiable credential");
var credential = new VerifiableCredential();
try
{
    credential = issuerService.IssueCredential(issuer, holder.DId);

    Console.WriteLine($"Issued Credential: {credential.CredentialType} for {credential.HolderDId}");
}
catch (ArgumentException ex)
{
    Log.Warning(ex, $"Issuing credential failed: {ex.Message}");
}
catch (Exception ex)
{
    Log.Fatal(ex, $"Unexpected error: {ex.Message}");
}

// Verifier validates the credential
WriteTitle("Verifier validates the credential");
try
{
    var validationResult = verifierService.ValidateCredential(credential, issuer.PublicKey);

    Log.Information(validationResult.IsValid
        ? "Credential is valid!"
        : $"Verification failed: {validationResult.ErrorMessage}");
}
catch (ArgumentException ex)
{
    Log.Warning(ex, $"Validation error: {ex.Message}");
}
catch (Exception ex)
{
    Log.Fatal(ex, $"Unexpected error: {ex.Message}");
}

Log.CloseAndFlush();

return;

static void WriteTitle(string title)
{
    Console.WriteLine();
    Console.WriteLine("***" + title.ToUpper());
}
