using System.Text.RegularExpressions;

namespace OpenID4VC_Prototype.Domain.Validators;

public static class DIdValidators
{
    public static bool IsValidDId(string dId)
    {
        var isValid = Regex.IsMatch(dId, @"^did:[a-zA-Z0-9]+:[a-zA-Z0-9\-\._]+$");

        if (!isValid)
            Log.Warning($"Invalid DID format detected: {dId}");

        return isValid;
    }
}
