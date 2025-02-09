using OpenID4VC_Prototype.Application.Models;
using OpenID4VC_Prototype.Domain.Models;

namespace OpenID4VC_Prototype.Application.Interfaces;

public interface IVerifierService
{
    ValidationResult ValidateCredential(VerifiableCredential credential, string issuerPublicKey);
}
