namespace Dinex.Core;

public class AccountActivationRequestDto
{
    public string Email { get; set; }
    public string? ActivationCode { get; set; }
}
