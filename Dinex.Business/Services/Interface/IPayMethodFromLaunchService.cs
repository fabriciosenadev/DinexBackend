namespace Dinex.Business
{
    public interface IPayMethodFromLaunchService
    {
        Task<PayMethodFromLaunchResponseDto> CreateAsync(PayMethodFromLaunchRequestDto payMethodFromLaunch, int launchId);
        Task<PayMethodFromLaunchResponseDto> UpdateAsync(PayMethodFromLaunchRequestDto payMethodFromLaunch, int launchId);
        Task SoftDeleteAsync(PayMethodFromLaunch payMethodFromLaunch);
        Task<PayMethodFromLaunchResponseDto> GetAsync(int launchId);
        Task<List<PayMethodFromLaunch>> ListAsync(List<int> launchIds);
        Task<PayMethodFromLaunch> GetByLaunchIdWithoutDtoAsync(int launchId);
    }
}
