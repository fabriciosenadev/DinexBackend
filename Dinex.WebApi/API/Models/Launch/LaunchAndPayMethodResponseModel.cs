namespace Dinex.WebApi.API.Models
{
    public class LaunchAndPayMethodResponseModel
    {
        public LaunchResponseModel Launch { get; set; }
        public PayMethodFromLaunchResponseModel? PayMethodFromLaunch { get; set; }
    }
}
