namespace Dinex.Business
{
    public interface ILaunchManager
    {
        Task<LaunchAndPayMethodResponseDto> CreateAsync(LaunchAndPayMethodRequestDto request, Guid userId);
        Task<LaunchAndPayMethodResponseDto> UpdateAsync(LaunchAndPayMethodRequestDto request, int launchId, Guid userId, bool isJustStatus);
        Task SoftDeleteAsync(int launchId);
        Task<LaunchAndPayMethodResponseDto> GetAsync(int launchId);
        Task<List<LaunchResponseDto>> ListLast(Guid userId);
        Task<LaunchConsolidationResponseDto> GetConsolidationByYearAndMonthAsync(int year, int month, Guid userId);
    }
}
