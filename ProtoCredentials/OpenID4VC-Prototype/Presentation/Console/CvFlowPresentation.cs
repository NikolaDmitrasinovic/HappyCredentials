using Mapster;
using OpenID4VC_Prototype.Application.Interfaces;
using OpenID4VC_Prototype.Application.Models;
using OpenID4VC_Prototype.Domain.Models;
using Serilog;

namespace OpenID4VC_Prototype.Presentation.Console;
public static class CvFlowPresentation
{
    public static void IssueCV(DecentralizedIdentifier issuer, IIssuerService issuerService, DecentralizedIdentifier holder, out VCDto credential)
    {
        try
        {
            var issuerDto = issuer.Adapt<DIdDto>();
            credential = issuerService.IssueCredential(issuerDto, holder.DId);

            Log.Information($"Issued Credential: {credential.CredentialType} for {credential.HolderDId}");
        }
        catch (ArgumentException ex)
        {
            Log.Warning(ex, $"Issuing credential failed: {ex.Message}");
            credential = new VCDto();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, $"Unexpected error: {ex.Message}");
            credential = new VCDto();
        }
    }
}
