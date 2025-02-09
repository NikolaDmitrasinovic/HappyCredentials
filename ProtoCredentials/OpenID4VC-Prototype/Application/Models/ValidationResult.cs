namespace OpenID4VC_Prototype.Application.Models;

public class ValidationResult(bool isValid, string errorMessage = "")
{
    public bool IsValid { get; set; } = isValid;
    public string ErrorMessage { get; set; } = errorMessage;
}
