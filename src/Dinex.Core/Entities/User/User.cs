namespace Dinex.Core;

public partial class User : Entity
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserActivatioStatus IsActive { get; set; }

    // directly convertion without automapper
    //public UserResponseDto ToUserReponseDto()
    //{
    //    return new UserResponseDto
    //    {
    //        Id = Id,
    //        FullName = FullName,
    //        Email = Email,
    //    };
    //}

    //public static implicit operator UserResponseDto(User user)
    //{
    //    return new UserResponseDto
    //    {
    //        Id = user.Id,
    //        FullName = user.FullName,
    //        Email = user.Email,
    //    };
    //}

    public static implicit operator User(UserRequestDto request)
    {
        return new User
        {
            FullName = request.FullName,
            Email = request.Email,
            Password = request.Password,
            IsActive = UserActivatioStatus.Inactive,
            CreatedAt = DateTime.UtcNow,
        };
    }
}
