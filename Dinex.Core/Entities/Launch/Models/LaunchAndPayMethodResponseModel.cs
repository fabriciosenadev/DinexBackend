namespace Dinex.Core
{
    public class LaunchAndPayMethodResponseModel
    {
        public LaunchResponseModel Launch { get; set; }
        public PayMethodFromLaunchResponseModel? PayMethodFromLaunch { get; set; }
    }
}
