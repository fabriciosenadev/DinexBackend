namespace Dinex.Core;

public class UserRequestDto
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string FullName { get; set; }

    public AccountRequestDto UserAccount { get; set; }
}
