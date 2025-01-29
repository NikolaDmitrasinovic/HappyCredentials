using System.Security.Cryptography;
using OpenID4VC_Prototype.Models;

namespace OpenID4VC_Prototype.Utils
{
    public class DIDGenerator
    {
        // Simplified DID generation for presentation purpose
        public static DecentralizedIdentifier GenerateDID()
        {
            var rsa = RSA.Create(2048);
            string publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
            string privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());

            return new DecentralizedIdentifier
            {
                DID = "did:example:" + Guid.NewGuid(),
                PublicKey = publicKey,
                PrivateKey = privateKey
            };
        }
    }
}
