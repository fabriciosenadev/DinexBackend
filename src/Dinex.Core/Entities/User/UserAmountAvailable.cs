namespace Dinex.Core
{
    public partial class UserAmountAvailable
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public decimal AmountAvailable { get; set; }
    }
}
