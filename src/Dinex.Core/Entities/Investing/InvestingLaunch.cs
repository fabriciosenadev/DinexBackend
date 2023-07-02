namespace Dinex.Core;

public class InvestingLaunch : Entity
{
    public Guid LaunchId { get; set; }
    public Applicable Applicable { get; set; }
    public InvestingActivity InvestingActivity { get; set; }
    public Guid ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal OperationPrice { get; set; }
    public int ProductOperationQuantity { get; set; }
    public Guid InvestingBrokerageId { get; set; }


}
