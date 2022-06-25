namespace Dinex.Business
{
    public interface IPayMethodFromLaunchService
    {
        Task<PayMethodFromLaunch> CreateAsync(PayMethodFromLaunch payMethodFromLaunch);
        Task<PayMethodFromLaunch> UpdateAsync(PayMethodFromLaunch payMethodFromLaunch);
        Task<bool> SoftDeleteAsync(PayMethodFromLaunch payMethodFromLaunch);
        Task<PayMethodFromLaunch> GetAsync(int launchId);
        Task<List<PayMethodFromLaunch>> ListAsync(List<int> launchIds);
    }
}
