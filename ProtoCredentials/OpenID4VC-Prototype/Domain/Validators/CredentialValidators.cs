using OpenID4VC_Prototype.Domain.Models;

namespace OpenID4VC_Prototype.Domain.Validators;

public static class CredentialValidators
{
    public static bool IsValidCredential(VerifiableCredential? credential)
    {
        if (credential == null) return false;
        if (!DIdValidators.IsValidDId(credential.IssuerDId)) return false;
        if (!DIdValidators.IsValidDId(credential.HolderDId)) return false;
        if (string.IsNullOrEmpty(credential.CredentialType)) return false;
        if (credential.Claims.Count == 0) return false;
        return !string.IsNullOrEmpty(credential.Signature);
    }
}
