namespace OpenID4VC_Prototype.Application.Models;

public class DIdDto
{
    public required string DId { get; set; }
    public required string PublicKey { get; set; }
    public required string PrivateKey { get; set; }
}
