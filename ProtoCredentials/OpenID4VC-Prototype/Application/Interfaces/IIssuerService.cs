using OpenID4VC_Prototype.Application.Models;

namespace OpenID4VC_Prototype.Application.Interfaces;

public interface IIssuerService
{
    VCDto IssueCredential(DIdDto issuer, string holderDId);
}
