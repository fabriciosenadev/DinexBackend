namespace Dinex.WebApi.Infra
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserInputModel, User>().ReverseMap();
            CreateMap<UserSearchResult, User>().ReverseMap();
        }
    }
}
