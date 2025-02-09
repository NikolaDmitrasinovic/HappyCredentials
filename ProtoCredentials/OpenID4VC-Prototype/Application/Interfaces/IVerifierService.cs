using OpenID4VC_Prototype.Application.Models;

namespace OpenID4VC_Prototype.Application.Interfaces;

public interface IVerifierService
{
    ValidationResult ValidateCredential(VCDto credential, string issuerPublicKey);
}
