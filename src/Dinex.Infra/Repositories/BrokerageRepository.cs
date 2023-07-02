namespace Dinex.Infra;

public class BrokerageRepository: Repository<InvestingBrokerage>, IBrokerageRepository
{
    private readonly IRepository<InvestingBrokerage> _repository;

    public BrokerageRepository(DinexBackendContext context, IRepository<InvestingBrokerage> repository) : base(context)
    {
        _repository = repository;
    }

    public async Task AddAsync(InvestingBrokerage brokerage)
    {
        await _repository.AddAsync(brokerage);
    }

    public async Task<InvestingBrokerage> GetByNameAsync(string BrokerName)
    {
        var result = await _context.InvestingBrokerages
            .FirstOrDefaultAsync(x => x.BrokerageName == BrokerName);
        return result;
    }
}
