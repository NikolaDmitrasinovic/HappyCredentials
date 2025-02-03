using OpenID4VC_Prototype.Models;
using OpenID4VC_Prototype.Utils;

namespace OpenID4VC_Prototype.Services
{
    public class VerifierService
    {
        public ValidationResult ValidateCredential(VerifiableCredential credential, string issuerPublicKey)
        {
            if (!IsValidCredential(credential))
                return new ValidationResult(false, "Credential is null");

            if (string.IsNullOrEmpty(issuerPublicKey))
                return new ValidationResult(false, "Issuer public key is missing");

            if (!DIdUtils.IsValidDId(credential.IssuerDId))
            {
                throw new ArgumentException($"Invalid issuer DID: {credential.IssuerDId}");
            }

            var isValid = CryptoUtils.VerifySignature(credential, issuerPublicKey);

            return isValid 
                ? new ValidationResult(true) 
                : new ValidationResult(false, "Signature verification failed");
        }

        private static bool IsValidCredential(VerifiableCredential credential)
        {
            if (credential == null) return false;
            if (!DIdUtils.IsValidDId(credential.IssuerDId)) return false;
            if (!DIdUtils.IsValidDId(credential.HolderDId)) return false;
            if (!DIdUtils.IsValidDId(credential.CredentialType)) return false;
            if (credential.Claims ==  null) return false;
            if (string.IsNullOrEmpty(credential.Signature)) return false;

            return true;
        }
    }
}
