namespace OpenID4VC_Prototype.Domain.Models;

public class VerifiableCredential
{
    public string IssuerDId { get; set; } = "";
    public string HolderDId { get; set; } = "";
    public string CredentialType { get; set; } = "";
    public Dictionary<string, string> Claims { get; set; } = new();
    public string Signature { get; set; } = "";
}
