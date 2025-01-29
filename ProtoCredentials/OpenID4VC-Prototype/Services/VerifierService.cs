using OpenID4VC_Prototype.Models;
using OpenID4VC_Prototype.Utils;

namespace OpenID4VC_Prototype.Services
{
    public class VerifierService
    {
        public bool ValidateCredential(VerifiableCredential credential,
            string issuerPublicKey)
        {
            return CryptoUtils.VerifySignature(credential, issuerPublicKey);
        }
    }
}
