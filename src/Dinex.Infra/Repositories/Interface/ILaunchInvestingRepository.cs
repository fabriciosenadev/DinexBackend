namespace Dinex.Infra;

public interface ILaunchInvestingRepository
{
    Task AddAsync(InvestingLaunch investingLaunch);
}
