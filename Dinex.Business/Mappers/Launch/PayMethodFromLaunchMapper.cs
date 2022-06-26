namespace Dinex.Business
{
    public class PayMethodFromLaunchMapper : Profile
    {
        public PayMethodFromLaunchMapper()
        {
            CreateMap<PayMethodFromLaunchRequestDto, PayMethodFromLaunch>().ReverseMap();
            CreateMap<PayMethodFromLaunchResponseDto, PayMethodFromLaunch>().ReverseMap();
        }
    }
}
