namespace OpenID4VC_Prototype.Application.Models;

public class VCDto
{
    public string IssuerDId { get; set; } = "";
    public string HolderDId { get; set; } = "";
    public string CredentialType { get; set; } = "";
    public Dictionary<string, string> Claims { get; set; } = [];
    public string Signature { get; set; } = "";
}
