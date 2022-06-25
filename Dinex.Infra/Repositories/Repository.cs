namespace Dinex.Infra
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbSet<T> _dbSet;
        protected readonly DinexBackendContext _context;

        public Repository(DinexBackendContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async virtual Task<int> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            return await SaveChangesAsync();
        }

        public async virtual Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async virtual Task<int> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await SaveChangesAsync();
        }
    }
}
