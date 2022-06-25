namespace Dinex.Infra
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserRequestModel, User>().ReverseMap();
            CreateMap<UserResponseModel, User>().ReverseMap();
        }
    }
}
