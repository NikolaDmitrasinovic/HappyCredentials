using Mapster;
using OpenID4VC_Prototype.Application.Interfaces;
using OpenID4VC_Prototype.Application.Models;
using OpenID4VC_Prototype.Domain.Interfaces;
using OpenID4VC_Prototype.Domain.Models;
using OpenID4VC_Prototype.Domain.Validators;
using Serilog;

namespace OpenID4VC_Prototype.Application.Services;

public class VerifierService(ICryptoService cryptoService, IJwtService jwtService) : IVerifierService
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

    public ValidationResult ValidateJwtVc(string jwtVc)
    {
        var isValid = jwtService.ValidateJwtVc(jwtVc);

        return isValid
            ? new ValidationResult(true)
            : new ValidationResult(false, "Signature verification failed");
    }
}
