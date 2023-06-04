namespace Dinex.Core
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public static explicit operator UserResponseDto(User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
            };
        }
    }
}
