namespace Dinex.Core
{
    public class LaunchAndPayMethodRequestDto
    {
        public LaunchRequestDto Launch { get; set; }
        public PayMethodFromLaunchRequestDto? PayMethodFromLaunch { get; set; }
    }
}
