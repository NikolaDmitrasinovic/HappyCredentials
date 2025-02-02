using System.Text.RegularExpressions;

namespace OpenID4VC_Prototype.Utils
{
    public static class DIdUtils
    {
        public static bool IsValidDId(string dId)
        {
            if (!string.IsNullOrEmpty(dId))
            {
                string patern = @"^did:[a-zA-Z0-9]+:[a-zA-Z0-9\-\._]+$";
                return Regex.IsMatch(dId, patern);
            }

            return false;
        }
    }
}
