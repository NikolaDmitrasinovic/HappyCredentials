using System.Security.Cryptography;
using OpenID4VC_Prototype.Core.Models;

namespace OpenID4VC_Prototype.Services;

public class DIdService
{
    public DecentralizedIdentifier GenerateDId(string method = "example")
    {
        return method switch
        {
            "ion" => GenerateIonDId(),
            "web" => GenerateWebDId(),
            "example" => GenerateExampleDId(),
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
        };
    }

    private DecentralizedIdentifier GenerateWebDId()
    {
        throw new NotImplementedException();
    }

    private DecentralizedIdentifier GenerateIonDId()
    {
        throw new NotImplementedException();
    }

    private DecentralizedIdentifier GenerateExampleDId()
    {
        var rsa = RSA.Create(2048);
        var publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
        var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());

        return new DecentralizedIdentifier
        {
            DId = "did:example:" + Guid.NewGuid(),
            PublicKey = publicKey,
            PrivateKey = privateKey
        };
    }
}
