namespace Dinex.WebApi.Entities
{
    public class Launch
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public LaunchStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
