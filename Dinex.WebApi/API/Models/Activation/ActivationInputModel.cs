namespace Dinex.WebApi.API.Models
{
    public class ActivationInputModel
    {
        public string Email { get; set; }
        public string? ActivationCode { get; set; }
    }
}
