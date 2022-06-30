namespace Dinex.Infra
{
    public interface ILaunchRepository : IRepository<Launch>
    {
        Task<Launch> GetByIdAsync(int launchId);
        Task<List<Launch>> ListLast(Guid userId);
        Task<int> CountByCategoryIdAsync(int categoryId, Guid userId);
    }
}
