using Mapster;
using OpenID4VC_Prototype.Application.Interfaces;
using OpenID4VC_Prototype.Application.Models;
using OpenID4VC_Prototype.Domain.Models;
using Serilog;

namespace OpenID4VC_Prototype.Presentation.Console;

public static class CvFlowPresentation
{
    public static void PresentVCFlow(DecentralizedIdentifier issuer, IIssuerService issuerService, DecentralizedIdentifier holder, IVerifierService verifierService)
    {
        // Issuing a verifiable credential
        WriteTitle("Issuing verifiable credential");
        var credential = new VCDto();
        try
        {
            var issuerDto = issuer.Adapt<DIdDto>();
            credential = issuerService.IssueCredential(issuerDto, holder.DId);

            System.Console.WriteLine($"Issued Credential: {credential.CredentialType} for {credential.HolderDId}");
        }
        catch (ArgumentException ex)
        {
            Log.Warning(ex, $"Issuing credential failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, $"Unexpected error: {ex.Message}");
        }

        // Verifier validates the credential
        WriteTitle("Verifier validates the credential");
        try
        {
            var validationResult = verifierService.ValidateCredential(credential, issuer.PublicKey);

            Log.Information(validationResult.IsValid
                ? "Credential is valid!"
                : $"Verification failed: {validationResult.ErrorMessage}");
        }
        catch (ArgumentException ex)
        {
            Log.Warning(ex, $"Validation error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, $"Unexpected error: {ex.Message}");
        }
    }

    static void WriteTitle(string title)
    {
        System.Console.WriteLine();
        System.Console.WriteLine("***" + title.ToUpper());
    }
}
