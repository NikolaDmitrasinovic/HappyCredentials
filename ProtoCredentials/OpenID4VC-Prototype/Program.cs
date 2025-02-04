using OpenID4VC_Prototype.Models;
using OpenID4VC_Prototype.Services;
using OpenID4VC_Prototype.Utils;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("../../logs/log.txt", rollingInterval: RollingInterval.Day)
    .Enrich.WithProperty("Application", "OpenID4VC Prototype")
    .CreateLogger();

var issuer = DIdGenerator.GenerateDId();
var holder = DIdGenerator.GenerateDId();
var verifier = DIdGenerator.GenerateDId();

Console.WriteLine($"Issuer DID: {issuer.DId}");
Console.WriteLine($"Holder DID: {holder.DId}");
Console.WriteLine($"Verifier DID: {verifier.DId}");

// Issuing a verifiable credential
WriteTitle("Issuing verifiable credential");
var credential = new VerifiableCredential();
try
{
    var issuerService = new IssuerService();
    credential = issuerService.IssuerCredential(issuer, holder.DId);

    Console.WriteLine($"Issuer Credential: {credential.CredentialType} for {credential.HolderDId}");
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
    var verifierService = new VerifierService();
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

return;

static void WriteTitle(string title)
{
    Console.WriteLine();
    Console.WriteLine("***" + title.ToUpper());
}
