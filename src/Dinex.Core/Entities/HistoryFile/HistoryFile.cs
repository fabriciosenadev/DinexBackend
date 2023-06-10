namespace Dinex.Core;

public partial class HistoryFile : Entity
{
    public Guid QueueId { get; set; }
    public Applicable Applicable { get; set; }
    public DateTime Date { get; set; }
    public InvestmentActivityType InvestmentActivityType { get; set; }
    public string Product { get; set; }
    public string Institution { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal OperationValue { get; set; }
}
