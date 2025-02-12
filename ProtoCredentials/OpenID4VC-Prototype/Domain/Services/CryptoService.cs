﻿using OpenID4VC_Prototype.Domain.Interfaces;
using OpenID4VC_Prototype.Domain.Models;
using System.Security.Cryptography;
using System.Text;

namespace OpenID4VC_Prototype.Domain.Services;

public class CryptoService : ICryptoService
{
    public string SignData(VerifiableCredential credential, string privateKeyBase64)
    {
        var rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKeyBase64), out _);

        var data = $"{credential.IssuerDId}:{credential.HolderDId}:{credential.CredentialType}";
        var signatureBytes = rsa.SignData(Encoding.UTF8.GetBytes(data), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

        return Convert.ToBase64String(signatureBytes);
    }

    public bool VerifySignature(VerifiableCredential credential, string publicKeyBase64)
    {
        if (string.IsNullOrEmpty(publicKeyBase64) || !IsBase64String(publicKeyBase64))
            throw new ArgumentException("Invalid public key format!");

        var rsa = RSA.Create();
        rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKeyBase64), out _);

        var data = $"{credential.IssuerDId}:{credential.HolderDId}:{credential.CredentialType}";
        var signatureBytes = Convert.FromBase64String(credential.Signature);

        return rsa.VerifyData(Encoding.UTF8.GetBytes(data), signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    }

    private static bool IsBase64String(string s)
    {
        var buffer = new Span<byte>(new byte[s.Length]);
        return Convert.TryFromBase64Chars(s, buffer, out _);
    }
}
