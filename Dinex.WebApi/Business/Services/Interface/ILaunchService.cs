namespace Dinex.WebApi.Business
{
    public interface ILaunchService
    {
        Task<(Launch, PayMethodFromLaunch?)> CreateAsync(Launch launch, PayMethodFromLaunch? payMethodFromLaunch);
        Task<(Launch, PayMethodFromLaunch?)> UpdateAsync(Launch launch, PayMethodFromLaunch? payMethodFromLaunch);
        Task<bool> DeleteAsync(int launchId);
        Task<(Launch, PayMethodFromLaunch?)> GetAsync(int launchId);
        Task<(List<Launch>, List<PayMethodFromLaunch>?)> ListAsync();
    }
}
