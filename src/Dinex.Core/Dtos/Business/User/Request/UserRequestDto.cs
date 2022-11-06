namespace Dinex.Core
{
    public class UserRequestDto
    {
        public Guid? Id { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public UserActivatioStatus? IsActive { get; set; }
    }
}
