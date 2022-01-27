namespace Dinex.WebApi.API.Models
{
    public class UserInputModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string VerifyPassword { get; set; }
    }
}
