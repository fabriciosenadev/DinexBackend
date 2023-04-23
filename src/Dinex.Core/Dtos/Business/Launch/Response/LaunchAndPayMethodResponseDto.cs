namespace Dinex.Core
{
    public class LaunchAndPayMethodResponseDto
    {
        public LaunchResponseDto Launch { get; set; }
        public PayMethodFromLaunchResponseDto? PayMethodFromLaunch { get; set; }
    }
}
