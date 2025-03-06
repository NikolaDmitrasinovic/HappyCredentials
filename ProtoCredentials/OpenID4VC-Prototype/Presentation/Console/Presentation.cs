using Mapster;
using OpenID4VC_Prototype.Application.Interfaces;
using OpenID4VC_Prototype.Application.Models;
using OpenID4VC_Prototype.Domain.Models;
using Serilog;

namespace OpenID4VC_Prototype.Presentation.Console;

public static class Presentation
{
    public static void ShowVcFlow(DecentralizedIdentifier issuer, IIssuerService issuerService, DecentralizedIdentifier holder, IVerifierService verifierService)
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

    public static void ShowJwtVcFlow(DecentralizedIdentifier issuer, IIssuerService issuerService, DecentralizedIdentifier holder, IVerifierService verifierService)
    {
        // Issuing JWT verifiable credential
        WriteTitle("Issuing verifiable credential via JWT");
        var jwtCredential = string.Empty;
        try
        {
            var issuerDto = issuer.Adapt<DIdDto>();
            jwtCredential = issuerService.IssueJwtCredential(issuerDto, holder.DId);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, $"Unexpected error: {ex.Message}");
        }

        // Validating JWT credential
        WriteTitle("Verifier receives JWT and validates credential");
        try
        {
            var validationResult = verifierService.ValidateJwtCredential(jwtCredential, issuer.PublicKey);

            Log.Information(validationResult.IsValid
                ? "Credential is valid!"
                : $"Verification failed: {validationResult.ErrorMessage}");
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, $"Unexpected error: {ex.Message}");
        }
    }

    private static void WriteTitle(string title)
    {
        System.Console.WriteLine("\n***" + title.ToUpper());
    }
}
