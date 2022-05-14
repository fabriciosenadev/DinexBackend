namespace Dinex.WebApi.Infra
{
    public class PayMethodFromLaunchMapper : Profile
    {
        public PayMethodFromLaunchMapper()
        {
            CreateMap<PayMethodFromLaunchRequestModel, PayMethodFromLaunch>().ReverseMap();
            CreateMap<PayMethodFromLaunchResponseModel, PayMethodFromLaunch>().ReverseMap();
        }
    }
}
