namespace Dinex.Core
{
    public struct LaunchAndPayMethodResponseDto
    {
        public LaunchResponseDto Launch { get; set; }
        public PayMethodFromLaunchResponseDto? PayMethodFromLaunch { get; set; }
    }
}
