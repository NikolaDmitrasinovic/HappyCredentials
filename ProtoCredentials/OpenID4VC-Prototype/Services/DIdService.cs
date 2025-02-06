using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace OpenID4VC_Prototype.Services;

public class DIdService(IOptions<DIdSettings> settings)
{
    public DecentralizedIdentifier GenerateDId(string method = "example")
    {
        return method switch
        {
            "ion" => GenerateIonDId(),
            "web" => GenerateWebDId(),
            "example" => GenerateExampleDId(settings.Value),
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

    private static DecentralizedIdentifier GenerateExampleDId(DIdSettings prefix)
    {
        var rsa = RSA.Create(prefix.DefaultKeySize);
        var publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
        var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());

        return new DecentralizedIdentifier
        {
            DId = prefix.DIDPrefix + Guid.NewGuid(),
            PublicKey = publicKey,
            PrivateKey = privateKey
        };
    }
}

public class DIdSettings
{
    public string DIDPrefix { get; set; }
    public int DefaultKeySize { get; set; }
}
