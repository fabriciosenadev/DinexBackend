namespace Dinex.Core
{
    public struct ActivationRequestDto
    {
        public string Email { get; set; }
        public string? ActivationCode { get; set; }
    }
}
