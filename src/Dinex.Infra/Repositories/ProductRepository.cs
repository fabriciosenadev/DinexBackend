namespace Dinex.Infra;

public class ProductRepository : Repository<InvestingProduct>, IProductRepository
{
    private readonly IRepository<InvestingProduct> _repository;
    public ProductRepository(DinexBackendContext context, IRepository<InvestingProduct> repository) : base(context)
    {
        _repository = repository;
    }

    public async Task AddAsync(InvestingProduct brokerage)
    {
        await _repository.AddAsync(brokerage);
    }

    public async Task<InvestingProduct> GetByNameAsync(string productCode)
    {
        var result = await _context.InvestingProducts
            .FirstOrDefaultAsync(x => x.ProductCode == productCode);
        return result;
    }
}
