namespace Dinex.WebApi.API.Models
{
    public class LaunchRequestModel
    {
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }
}
