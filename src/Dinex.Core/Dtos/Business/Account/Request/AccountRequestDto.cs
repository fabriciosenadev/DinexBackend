namespace Dinex.Core;

public class AccountRequestDto
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public required string ConfirmPassword { get; set; }

    public AccountActivatioStatus? IsActive { get; set; }
}
