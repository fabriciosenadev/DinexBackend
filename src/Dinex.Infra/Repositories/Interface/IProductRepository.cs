namespace Dinex.Infra;

public interface IProductRepository
{
    Task AddAsync(InvestingProduct brokerage);
    Task<InvestingProduct> GetByNameAsync(string productCode);
}
