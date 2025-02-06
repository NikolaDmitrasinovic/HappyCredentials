using System.Security.Cryptography;
using OpenID4VC_Prototype.Core.Models;

namespace OpenID4VC_Prototype.Utils;

public class DIdGenerator
{
    // Simplified DId generation for presentation purpose
    public static DecentralizedIdentifier GenerateDId()
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
