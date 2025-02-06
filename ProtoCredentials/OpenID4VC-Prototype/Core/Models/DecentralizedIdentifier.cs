namespace OpenID4VC_Prototype.Core.Models;

public class DecentralizedIdentifier
{
    public required string DId { get; set; }
    public required string PublicKey { get; set; }
    public required string PrivateKey { get; set; }
}
