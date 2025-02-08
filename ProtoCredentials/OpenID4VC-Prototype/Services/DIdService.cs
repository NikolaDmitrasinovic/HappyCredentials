using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using OpenID4VC_Prototype.Infrastructure.Configurations;

namespace OpenID4VC_Prototype.Services;

public class DIdService(IOptions<DIdConfiguration> options)
{
    public DecentralizedIdentifier GenerateDId(string method = "example")
    {
        return method switch
        {
            "ion" => GenerateIonDId(),
            "web" => GenerateWebDId(),
            "example" => GenerateExampleDId(options.Value),
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
        };
    }

    private static DecentralizedIdentifier GenerateWebDId()
    {
        throw new NotImplementedException();
    }

    private static DecentralizedIdentifier GenerateIonDId()
    {
        throw new NotImplementedException();
    }

    private static DecentralizedIdentifier GenerateExampleDId(DIdConfiguration configuration)
    {
        var rsa = RSA.Create(configuration.DefaultKeySize);
        var publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
        var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());

        return new DecentralizedIdentifier
        {
            DId = configuration.DIdPrefix + Guid.NewGuid(),
            PublicKey = publicKey,
            PrivateKey = privateKey
        };
    }
}
