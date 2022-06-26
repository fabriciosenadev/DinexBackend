namespace Dinex.Infra
{
    public interface IRepository<T> where T : class
    {
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> SaveChangesAsync();
    }
}
