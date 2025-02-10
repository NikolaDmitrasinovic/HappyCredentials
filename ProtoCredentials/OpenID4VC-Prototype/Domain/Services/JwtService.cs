using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OpenID4VC_Prototype.Domain.Interfaces;
using OpenID4VC_Prototype.Domain.Models;

namespace OpenID4VC_Prototype.Domain.Services;
public class JwtService : IJwtService
{
    public string CreateJwtVc(VerifiableCredential credential, string privateKeyBase64)
    {
        //var claims = new[]
        //{
        //    new Claim("iss", credential.IssuerDId),
        //    new Claim("sub", credential.HolderDId),
        //    new Claim("type", credential.CredentialType)
        //};

        var rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKeyBase64), out _);

        var tokenData = new SecurityTokenDescriptor
        {
            Issuer = credential.IssuerDId,
            Claims = new Dictionary<string, object>
            {
                {"sub", credential.HolderDId },
                {"credential", credential.Claims }
            },
            SigningCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
        };

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(tokenData);
        return handler.WriteToken(token);
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
