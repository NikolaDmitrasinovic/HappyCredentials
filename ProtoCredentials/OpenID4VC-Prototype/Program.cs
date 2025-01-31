using OpenID4VC_Prototype.Services;
using OpenID4VC_Prototype.Utils;

var issuer = DIdGenerator.GenerateDId();
var holder = DIdGenerator.GenerateDId();
var verifier = DIdGenerator.GenerateDId();

Console.WriteLine($"Issuer DId: {issuer.DId}");
Console.WriteLine($"Holder DId: {holder.DId}");
Console.WriteLine($"Verifier DId: {verifier.DId}");

// Issuing a verifiable credential
WriteTitle("Issuing verifiable credential");
var issuerService = new IssuerService();
var credential = issuerService.IssuerCredential(issuer, holder.DId);

Console.WriteLine($"Issuer Credential: {credential.CredentialType} for {credential.HolderDId}");

// Verifier validates the credential
WriteTitle("Verifier validates the credential");
var verifierService = new VerifierService();
var isValid = verifierService.ValidateCredential(credential, issuer.PublicKey);

Console.WriteLine($"Credential Valid: {isValid}");
return;

static void WriteTitle(string title)
{
    Console.WriteLine();
    Console.WriteLine("***" + title.ToUpper());
}
