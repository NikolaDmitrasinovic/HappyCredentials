using OpenID4VC_Prototype.Domain.Models;

namespace OpenID4VC_Prototype.Domain.Interfaces;

public interface ICryptoService
{
    string SignData(VerifiableCredential credential, string privateKeyBase64);
    bool VerifySignature(VerifiableCredential credential, string publicKeyBase64);
}
