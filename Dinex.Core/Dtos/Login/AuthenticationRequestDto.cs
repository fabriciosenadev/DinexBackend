namespace Dinex.Core
{
    public class AuthenticationRequestDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
