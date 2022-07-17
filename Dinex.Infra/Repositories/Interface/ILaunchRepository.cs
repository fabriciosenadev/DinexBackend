namespace Dinex.Infra
{
    public interface ILaunchRepository : IRepository<Launch>
    {
        Task<Launch> GetByIdAsync(int launchId);
        Task<List<Launch>> ListLast(Guid userId);
        Task<int> CountByCategoryIdAsync(int categoryId, Guid userId);
        Task<decimal> GetSumAmountByStatus(List<int> categoryIds, Guid userId, LaunchStatus launchStatus, DateTime startDate, DateTime endDate);
        Task<List<Launch>> ListAsync(DateTime startDate, DateTime endDate, Guid userId);
    }
}
