using OpenID4VC_Prototype.Core.Interfaces;
using OpenID4VC_Prototype.Core.Models;
using OpenID4VC_Prototype.Utils;
using Serilog;

namespace OpenID4VC_Prototype.Services;

public class VerifierService(CryptoService cryptoService) : IVerifierService
{
    private readonly CryptoService _cryptoService = cryptoService;

    public ValidationResult ValidateCredential(VerifiableCredential credential, string issuerPublicKey)
    {
        Log.Information($"Verifying credential for holder DID: {credential.HolderDId}");

        if (!CredentialUtils.IsValidCredential(credential))
            return new ValidationResult(false, "Invalid Credential provided");

        if (string.IsNullOrEmpty(issuerPublicKey))
            return new ValidationResult(false, "Issuer public key is missing");

        var isValid = _cryptoService.VerifySignature(credential, issuerPublicKey);

        return isValid
            ? new ValidationResult(true)
            : new ValidationResult(false, "Signature verification failed");
    }
}
