using OpenID4VC_Prototype.Models;

namespace OpenID4VC_Prototype.Utils;

public static class CredentialUtils
{
    public static bool IsValidCredential(VerifiableCredential? credential)
    {
        if (credential == null) return false;
        if (!DIdUtils.IsValidDId(credential.IssuerDId)) return false;
        if (!DIdUtils.IsValidDId(credential.HolderDId)) return false;
        if (string.IsNullOrEmpty(credential.CredentialType)) return false;
        if (credential.Claims.Count == 0) return false;
        return !string.IsNullOrEmpty(credential.Signature);
    }
}
