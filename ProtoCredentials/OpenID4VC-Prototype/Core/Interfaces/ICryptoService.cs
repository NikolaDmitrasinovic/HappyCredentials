namespace OpenID4VC_Prototype.Core.Interfaces;

public interface ICryptoService
{
    string SignData(VerifiableCredential credential, string privateKeyBase64);
    bool VerifySignature(VerifiableCredential credential, string publicKeyBase64);
}
