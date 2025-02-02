﻿using System.Security.Cryptography;
using System.Text;
using OpenID4VC_Prototype.Models;

namespace OpenID4VC_Prototype.Utils
{
    public class CryptoUtils
    {
        public static string SignData(VerifiableCredential credential, string privateKeyBase64)
        {
            var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKeyBase64), out _);

            var data = $"{credential.IssuerDId}:{credential.HolderDId}:{credential.CredentialType}";
            var signatureBytes = rsa.SignData(Encoding.UTF8.GetBytes(data), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            return Convert.ToBase64String(signatureBytes);
        }

        public static bool VerifySignature(VerifiableCredential credential, string publicKeyBase64)
        {
            var rsa = RSA.Create();
            rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKeyBase64), out _);

            var data = $"{credential.IssuerDId}:{credential.HolderDId}:{credential.CredentialType}";
            var signatureBytes = Convert.FromBase64String(credential.Signature);

            return rsa.VerifyData(Encoding.UTF8.GetBytes(data), signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }
    }
}
