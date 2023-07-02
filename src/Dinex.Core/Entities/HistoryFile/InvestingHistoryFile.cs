namespace Dinex.Core;

public partial class InvestingHistoryFile : Entity
{
    public Guid QueueId { get; set; }
    public Applicable Applicable { get; set; }
    public DateTime Date { get; set; }
    public InvestingActivity ActivityType { get; set; }
    public string Product { get; set; }
    public string Institution { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal OperationValue { get; set; }
}
