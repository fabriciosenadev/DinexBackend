namespace Dinex.Core
{
    public class UserResetPasswordDto : ActivationRequestDto
    {
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
