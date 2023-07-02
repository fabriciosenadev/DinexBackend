namespace Dinex.Core
{
    public partial class Launch : Entity
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public TransactionActivity Activity { get; set; }
        //public string? Description { get; set; }
        //public int CategoryId { get; set; }
        //public decimal Amount { get; set; }
        //public LaunchStatus Status { get; set; }
    }
}
