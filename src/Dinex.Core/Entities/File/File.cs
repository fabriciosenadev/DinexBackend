namespace Dinex.Core;

public class File : Entity
{
    public Guid QueueId { get; set; }
    public Applicable Applicable { get; set; }
    public DateTime Date { get; set; }
    public InvestmentActivityType InvestmentActivityType { get; set; }
    public required string Product { get; set; }
    public required string Institution { get; set; }
    public int Qauantity { get; set; }
    public Decimal UnitPrice { get; set; }
    public Decimal OperationValue { get; set; }
}
