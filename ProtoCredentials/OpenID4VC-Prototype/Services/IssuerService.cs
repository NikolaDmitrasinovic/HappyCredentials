using OpenID4VC_Prototype.Models;
using OpenID4VC_Prototype.Utils;

namespace OpenID4VC_Prototype.Services
{
    public class IssuerService
    {
        public VerifiableCredential IssuerCredential
            (DecentralizedIdentifier issuer, string holderDID)
        {
            var credential = new VerifiableCredential
            {
                IssuerDID = issuer.DID,
                HolderDID = holderDID,
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
