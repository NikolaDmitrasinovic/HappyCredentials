using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using OpenID4VC_Prototype.Domain.Models;

namespace OpenID4VC_Prototype.Domain.Services;
public class JwtService
{
    public string CreateJwtVc(VerifiableCredential verifiableCredential)
    {
        var claims = new[]
        {
            new Claim("issuer", verifiableCredential.IssuerDId),
            new Claim("holder", verifiableCredential.HolderDId),
            new Claim("type", verifiableCredential.CredentialType)
        };

        var token = new JwtSecurityToken(
            issuer: verifiableCredential.IssuerDId,
            claims: claims
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
