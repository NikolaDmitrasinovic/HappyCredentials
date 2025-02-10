using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using OpenID4VC_Prototype.Domain.Interfaces;
using OpenID4VC_Prototype.Domain.Models;

namespace OpenID4VC_Prototype.Domain.Services;
public class JwtService : IJwtService
{
    public string CreateJwtVc(VerifiableCredential verifiableCredential)
    {
        var claims = new[]
        {
            new Claim("iss", verifiableCredential.IssuerDId),
            new Claim("sub", verifiableCredential.HolderDId),
            new Claim("type", verifiableCredential.CredentialType)
        };

        var token = new JwtSecurityToken(
            issuer: verifiableCredential.IssuerDId,
            claims: claims
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool ValidateJwtVc(string jwtVc)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true
        };

        try
        {
            tokenHandler.ValidateToken(jwtVc, validationParameters, out _);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
