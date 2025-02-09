using OpenID4VC_Prototype.Domain.Models;

namespace OpenID4VC_Prototype.Application.Interfaces;

public interface IIssuerService
{
    VerifiableCredential IssueCredential(DecentralizedIdentifier issuer, string holderDId);
}
