using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using OpenID4VC_Prototype.Domain.Interfaces;
using OpenID4VC_Prototype.Domain.Models;

namespace OpenID4VC_Prototype.Domain.Services;

public class JwtService : IJwtService
{
    public string CreateJwtVc(VerifiableCredential credential, string privateKey)
    {
        var rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);

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
            SigningCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
        };

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    public bool ValidateJwtVc(string jwtVc, string publicKey)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var rsa = RSA.Create();
        rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidIssuer = "did:example:issuer",
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new RsaSecurityKey(rsa),
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
