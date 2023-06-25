namespace Dinex.Core;

public class Account : Entity
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public AccountActivatioStatus IsActive { get; set; }

    public static implicit operator Account(AccountRequestDto request)
    {
        return new Account
        {
            Email = request.Email,
            Password = request.Password,
            IsActive = AccountActivatioStatus.Inactive,
            CreatedAt = DateTime.UtcNow,
        };
    }
}
