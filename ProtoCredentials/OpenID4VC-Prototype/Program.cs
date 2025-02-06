using OpenID4VC_Prototype.Core.Models;
using OpenID4VC_Prototype.Logging;
using OpenID4VC_Prototype.Services;
using Serilog;

LogConfigurator.Configure();

var cryptoService = new CryptoService();
var issuerService = new IssuerService(cryptoService);
var verifierService = new VerifierService(cryptoService);

var dIdService = new DIdService();
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
