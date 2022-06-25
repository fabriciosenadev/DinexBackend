namespace Dinex.Core
{
    public class LaunchAndPayMethodRequestModel
    {
        public LaunchRequestModel Launch { get; set; }
        public PayMethodFromLaunchRequestModel? PayMethodFromLaunch { get; set; }
    }
}
