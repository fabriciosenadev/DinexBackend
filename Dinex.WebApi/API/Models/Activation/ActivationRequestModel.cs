namespace Dinex.WebApi.API.Models
{
    public class ActivationRequestModel
    {
        public string Email { get; set; }
        public string? ActivationCode { get; set; }
    }
}
