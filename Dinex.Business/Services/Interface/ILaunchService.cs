namespace Dinex.Business
{
    public interface ILaunchService
    {
        Task<Launch> CreateAsync(Launch launch, Guid userId);
        Task<Launch> UpdateAsync(Launch launch);
        Task SoftDeleteAsync(Launch launch);
        Task<List<Launch>> ListAsync(DateTime startDate, DateTime endDate, Guid userId);
        Task<List<Launch>> ListLast(Guid userId);
        Task CheckExistsByCategoryIdAsync(int categoryId, Guid userId);
        Task<Launch> GetByIdAsync(int launchId);
        Task<decimal> GetSumAmountByStatus(List<int> categoryIds, Guid userId, LaunchStatus launchStatus, DateTime startDate, DateTime endDate);
    }
}
