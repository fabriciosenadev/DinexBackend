namespace Dinex.Core
{
    public struct LaunchAndPayMethodRequestDto
    {
        public LaunchRequestDto Launch { get; set; }
        public PayMethodFromLaunchRequestDto? PayMethodFromLaunch { get; set; }
    }
}
