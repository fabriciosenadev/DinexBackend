namespace Dinex.Infra;

public interface IBrokerageRepository
{
    Task AddAsync(InvestingBrokerage brokerage);
    Task<InvestingBrokerage> GetByNameAsync(string BrokerName);

}
