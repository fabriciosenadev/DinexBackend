namespace Dinex.Business
{
    public interface ILaunchService
    {
        Task<Launch> CreateAsync(Launch launch, Guid userId);
        Task<Launch> UpdateAsync(Launch launch, int launchId, Guid userId, bool isJustStatus, DateTime createdAt, LaunchStatus? newStatus);
        Task SoftDeleteAsync(Launch launch);
        Task<List<LaunchAndPayMethodResponseDto>> ListAsync();
        Task<List<Launch>> ListLast(Guid userId);
        Task CheckExistsByCategoryIdAsync(int categoryId, Guid userId);
        Task<Launch> GetByIdAsync(int launchId);
    }
}
