using OpenID4VC_Prototype.Core.Models;

namespace OpenID4VC_Prototype.Core.Interfaces;
public interface ICryptoService
{
    string SignData(VerifiableCredential credential, string privateKeyBase64);
    bool VerifyData(VerifiableCredential credential, string publicKeyBase64);
}
