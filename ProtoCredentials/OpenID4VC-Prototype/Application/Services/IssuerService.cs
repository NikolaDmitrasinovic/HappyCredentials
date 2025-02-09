using Mapster;
using OpenID4VC_Prototype.Application.Interfaces;
using OpenID4VC_Prototype.Application.Models;
using OpenID4VC_Prototype.Domain.Interfaces;
using OpenID4VC_Prototype.Domain.Models;
using OpenID4VC_Prototype.Domain.Validators;

namespace OpenID4VC_Prototype.Application.Services;

public class IssuerService(ICryptoService cryptoService) : IIssuerService
{
    public VCDto IssueCredential(DIdDto issuer, string holderDId)
    {
        Log.Information($"Issuing credential for holder DID: {holderDId}");

        if (!DIdValidators.IsValidDId(holderDId))
            throw new ArgumentException($"Invalid holder DID: {holderDId}");

        var credential = new VerifiableCredential
        {
            IssuerDId = issuer.DId,
            HolderDId = holderDId,
            CredentialType = "Diploma",
            Claims = new Dictionary<string, string>
            {
                { "University", "PMF" },
                { "Curriculum", "Mathematics" }
            }
        };

        credential.Signature = cryptoService.SignData(credential, issuer.PrivateKey);

        return credential.Adapt<VCDto>();
    }
}
