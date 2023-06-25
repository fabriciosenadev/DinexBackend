namespace Dinex.Core;

public partial class User : Entity
{
    public required string FullName { get; set; }

    public required Account UserAccount { get; set; }

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
            UserAccount = request.UserAccount
        };
    }
}
