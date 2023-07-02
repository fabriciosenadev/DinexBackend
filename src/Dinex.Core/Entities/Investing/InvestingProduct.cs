namespace Dinex.Core;

public class InvestingProduct : Entity
{
    public InvestingProductTypes? ProductType { get; set; }
    public string ProductCode { get; set; }
    public string CompanyName { get; set; }
}
