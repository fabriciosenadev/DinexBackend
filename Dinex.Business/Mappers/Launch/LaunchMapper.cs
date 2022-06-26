namespace Dinex.Business
{
    public class LaunchMapper : Profile
    {
        public LaunchMapper()
        {
            CreateMap<LaunchRequestDto, Launch>().ReverseMap();
            CreateMap<LaunchResponseDto, Launch>().ReverseMap();
        }
    }
}
