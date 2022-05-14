namespace Dinex.WebApi.Infra
{
    public class LaunchMapper : Profile
    {
        public LaunchMapper()
        {
            CreateMap<LaunchRequestModel, Launch>().ReverseMap();
            CreateMap<LaunchResponseModel, Launch>().ReverseMap();
        }
    }
}
