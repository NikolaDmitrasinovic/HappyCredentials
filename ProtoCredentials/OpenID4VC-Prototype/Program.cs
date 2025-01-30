using OpenID4VC_Prototype.Services;
using OpenID4VC_Prototype.Utils;

namespace OpenID4VC_Prototype
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var issuer = DIDGenerator.GenerateDID();
            var holder = DIDGenerator.GenerateDID();
            var verifier = DIDGenerator.GenerateDID();

            Console.WriteLine($"Issuer DID: {issuer.DID}");
            Console.WriteLine($"Holder DID: {holder.DID}");
            Console.WriteLine($"Verifier DID: {verifier.DID}");

            // Issuing a verifiable credential
            WriteTitle("Issuing verifiable credential");
            var issuerService = new IssuerService();
            var credential = issuerService.IssuerCredential(issuer, holder.DID);

            Console.WriteLine($"Issuer Credential: {credential.CredentialType} for {credential.HolderDID}");

            // Verifier validates the credential
            WriteTitle("Verifier validates the credential");
            var verifierService = new VerifierService();
            var isValid = verifierService.ValidateCredential(credential, issuer.PublicKey);

            Console.WriteLine($"Credential Valid: {isValid}");
        }

        private static void WriteTitle(string title)
        {
            Console.WriteLine();
            Console.WriteLine("***" + title.ToUpper());
        }
    }
}
