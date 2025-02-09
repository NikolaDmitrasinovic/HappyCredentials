using Mapster;
using OpenID4VC_Prototype.Application.Interfaces;
using OpenID4VC_Prototype.Application.Models;
using OpenID4VC_Prototype.Domain.Interfaces;
using OpenID4VC_Prototype.Domain.Models;
using OpenID4VC_Prototype.Domain.Validators;

namespace OpenID4VC_Prototype.Application.Services;

public class VerifierService(ICryptoService cryptoService) : IVerifierService
{
    public ValidationResult ValidateCredential(VCDto credential, string issuerPublicKey)
    {
        Log.Information($"Verifying credential for holder DID: {credential.HolderDId}");

        var domainCredential = credential.Adapt<VerifiableCredential>();

        if (!CredentialValidators.IsValidCredential(domainCredential))
            return new ValidationResult(false, "Invalid Credential provided");

        if (string.IsNullOrEmpty(issuerPublicKey))
            return new ValidationResult(false, "Issuer public key is missing");

        var isValid = cryptoService.VerifySignature(domainCredential, issuerPublicKey);

        return isValid
            ? new ValidationResult(true)
            : new ValidationResult(false, "Signature verification failed");
    }
}
