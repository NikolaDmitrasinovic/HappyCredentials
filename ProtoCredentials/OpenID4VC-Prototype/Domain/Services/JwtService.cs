using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using OpenID4VC_Prototype.Domain.Interfaces;
using OpenID4VC_Prototype.Domain.Models;

namespace OpenID4VC_Prototype.Domain.Services;
public class JwtService(RSA privateKey) : IJwtService
{
    private readonly RSA _privateKey = privateKey;

    public string CreateJwtVc(VerifiableCredential credential)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = credential.IssuerDId,
            Claims = new Dictionary<string, object>
            {
                {"sub", credential.HolderDId },
                {"credential", credential.Claims },
                {"iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds()}
            },
            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = new SigningCredentials(new RsaSecurityKey(_privateKey), SecurityAlgorithms.RsaSha256)
        };

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    public bool ValidateJwtVc(string jwtVc, RSA publicKey)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "did:example:issuer",
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new RsaSecurityKey(publicKey),
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
