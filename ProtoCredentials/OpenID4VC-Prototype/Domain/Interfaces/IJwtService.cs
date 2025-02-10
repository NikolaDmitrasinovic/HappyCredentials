using OpenID4VC_Prototype.Domain.Models;

namespace OpenID4VC_Prototype.Domain.Interfaces;
public interface IJwtService
{
    string CreateJwtVc(VerifiableCredential verifiableCredential);
}
