namespace Dinex.Infra;

public class LaunchInvestingRepository : Repository<InvestingLaunch>, ILaunchInvestingRepository
{
    private readonly IRepository<InvestingLaunch> _repository;
    public LaunchInvestingRepository(DinexBackendContext context, IRepository<InvestingLaunch> repository) : base(context)
    {
        _repository = repository;
    }

    public async Task AddAsync(InvestingLaunch investingLaunch)
    {
        await _repository.AddAsync(investingLaunch);
    }
}
