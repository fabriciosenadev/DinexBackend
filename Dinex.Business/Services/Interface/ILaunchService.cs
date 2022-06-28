namespace Dinex.Business
{
    public interface ILaunchService
    {
        Task<LaunchAndPayMethodResponseDto> CreateAsync(LaunchAndPayMethodRequestDto request, Guid userId);
        Task<LaunchAndPayMethodResponseDto> UpdateAsync(LaunchAndPayMethodRequestDto request, int launchId, Guid userId, bool isJustStatus);
        Task SoftDeleteAsync(int launchId);
        Task<LaunchAndPayMethodResponseDto> GetAsync(int launchId);
        Task<List<LaunchAndPayMethodResponseDto>> ListAsync();
        Task<List<LaunchResponseDto>> ListLast(Guid userId);
    }
}
