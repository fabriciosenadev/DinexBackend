namespace Dinex.Core
{
    public class UserRequestDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string ConfirmPassword { get; set; }

        public UserActivatioStatus? IsActive { get; set; }
    }
}
