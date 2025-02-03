using OpenID4VC_Prototype.Models;
using OpenID4VC_Prototype.Utils;

namespace OpenID4VC_Prototype.Services
{
    public class VerifierService
    {
        public ValidationResult ValidateCredential(VerifiableCredential credential, string issuerPublicKey)
        {
            if (!CredentialUtils.IsValidCredential(credential))
                return new ValidationResult(false, "Invalid Credential provided");

            if (string.IsNullOrEmpty(issuerPublicKey))
                return new ValidationResult(false, "Issuer public key is missing");

            var isValid = CryptoUtils.VerifySignature(credential, issuerPublicKey);

            return isValid
                ? new ValidationResult(true)
                : new ValidationResult(false, "Signature verification failed");
        }
    }
}
