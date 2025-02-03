using OpenID4VC_Prototype.Models;
using OpenID4VC_Prototype.Utils;

namespace OpenID4VC_Prototype.Services
{
    public class IssuerService
    {
        public VerifiableCredential IssuerCredential(DecentralizedIdentifier issuer, string holderDId)
        {
            if (!DIdUtils.IsValidDId(holderDId)) 
                throw new ArgumentNullException($"Invalid holder DID: {holderDId}");

            var credential = new VerifiableCredential
            {
                IssuerDId = issuer.DId,
                HolderDId = holderDId,
                CredentialType = "Diploma",
                Claims = new Dictionary<string, string>
                {
                    { "University", "PMF" },
                    { "Curriculum", "Mathematics" }
                }
            };

            credential.Signature = CryptoUtils.SignData(credential, issuer.PrivateKey);

            return credential;
        }
    }
}
