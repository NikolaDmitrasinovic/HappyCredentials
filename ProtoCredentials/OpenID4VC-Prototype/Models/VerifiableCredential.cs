namespace OpenID4VC_Prototype.Models
{
    public class VerifiableCredential
    {
        public string IssuerDID { get; set; }
        public string HolderDID { get; set; }
        public string CredentialType { get; set; }
        public Dictionary<string, string> Claims { get; set; }
        public string Signature { get; set; }
    }
}
