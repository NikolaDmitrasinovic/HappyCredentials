namespace OpenID4VC_Prototype.Core.Interfaces;

public interface IVerifierService
{
    ValidationResult ValidateCredential(VerifiableCredential credential, string issuerPublicKey);
}
