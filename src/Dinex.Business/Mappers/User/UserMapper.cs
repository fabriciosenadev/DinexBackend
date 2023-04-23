namespace Dinex.Business
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserRequestDto, User>().ReverseMap();
            CreateMap<UserResponseDto, User>().ReverseMap();
            CreateMap<UserAmountAvailableResponseDto, UserAmountAvailable>().ReverseMap();
        }
    }
}
