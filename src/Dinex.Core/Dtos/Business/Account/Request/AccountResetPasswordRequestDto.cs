namespace Dinex.Core;

public class AccountResetPasswordRequestDto : AccountActivationRequestDto
{
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
}
