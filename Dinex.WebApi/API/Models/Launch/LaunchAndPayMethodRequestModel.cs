namespace Dinex.WebApi.API.Models
{
    public class LaunchAndPayMethodRequestModel
    {
        public LaunchRequestModel Launch { get; set; }
        public PayMethodFromLaunchRequestModel? PayMethodFromLaunch { get; set; }
    }
}
