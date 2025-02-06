using OpenID4VC_Prototype.Core.Models;

namespace OpenID4VC_Prototype.Core.Interfaces;
public interface IIssuerService
{
    VerifiableCredential IssueCredential(DecentralizedIdentifier issuer, string holderDId);
}
