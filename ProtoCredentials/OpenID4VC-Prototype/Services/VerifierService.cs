using OpenID4VC_Prototype.Models;
using OpenID4VC_Prototype.Utils;
using System.Text.RegularExpressions;

namespace OpenID4VC_Prototype.Services
{
    public class VerifierService
    {
        public ValidationResult ValidateCredential(VerifiableCredential credential, string issuerPublicKey)
        {
            if (credential == null)
                return new ValidationResult(false, "Credential is null");

            if (string.IsNullOrEmpty(issuerPublicKey))
                return new ValidationResult(false, "Issuer public key is missing");

            if (!IsValidDId(credential.IssuerDId))
                throw new ArgumentException($"Invalid issuer DID: {credential.IssuerDId}");

            var isValid = CryptoUtils.VerifySignature(credential, issuerPublicKey);

            return isValid 
                ? new ValidationResult(true) 
                : new ValidationResult(false, "Signature verification failed");
        }

        private static bool IsValidDId(string dId)
        {
            if (!string.IsNullOrEmpty(dId))
            {
                string patern = @"^did:[a-zA-Z0-9]+:[a-zA-Z0-9\-\.\_]+$";
                return Regex.IsMatch(dId, patern);
            }

            return false;
        }
    }
}
