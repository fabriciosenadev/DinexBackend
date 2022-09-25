namespace Dinex.Infra
{
    public class LaunchRepository : Repository<Launch>, ILaunchRepository
    {
        public LaunchRepository(DinexBackendContext context) : base(context)
        {

        }

        public async Task<int> CountByCategoryIdAsync(int categoryId, Guid userId)
        {
            var result = await _context.Launches
                .Where(x =>
                    x.CategoryId.Equals(categoryId) && x.UserId.Equals(userId)
                )
                .CountAsync();
            return result;
        }

        public async Task<Launch> GetByIdAsync(int launchId)
        {
            var result = await _context.Launches
                .Where(x => x.Id.Equals(launchId))
                .FirstAsync();

            return result;
        }

        public async Task<decimal> GetSumAmountByStatus(
            List<int> categoriesId,
            Guid userId,
            LaunchStatus launchStatus,
            DateTime startDate,
            DateTime endDate)
        {
            var list = await _context.Launches
                .Where(x =>
                    x.Status.Equals(launchStatus) &&
                    x.UserId.Equals(userId) &&
                    x.Date >= startDate &&
                    x.Date <= endDate &&
                    x.DeletedAt == null &&
                    categoriesId.Contains(x.CategoryId)
                ).ToListAsync();

            var result = list.Sum(x => x.Amount);
            return result;
        }

        public async Task<List<Launch>> ListAsync(DateTime startDate, DateTime endDate, Guid userId)
        {
            var result = await _context.Launches
                .Where(x => 
                    x.UserId.Equals(userId) && 
                    x.Date >= startDate && 
                    x.Date <= endDate && 
                    x.DeletedAt == null
                ).OrderByDescending(x => x.Date)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return result;
        }

        public async Task<List<Launch>> ListLast(Guid userId)
        {
            var result = await _context.Launches
                .Where(x => x.UserId.Equals(userId) && x.DeletedAt == null)
                .OrderByDescending(x => x.Date)
                .OrderByDescending(x => x.CreatedAt)
                .Take(8)
                .ToListAsync();

            return result;

        }

        public async Task<decimal> GetLaunchesSumByCategoriesIdAndStatus(Guid userId, List<int> categoriesId, LaunchStatus status)
        {
            var result = await _context.Launches
                .Where(x => 
                    x.UserId.Equals(userId) &&
                    categoriesId.Contains(x.CategoryId) &&
                    x.DeletedAt == null &&
                    x.Status == status
                ).Select(x => x.Amount)
                .ToListAsync();

            return result.Sum();
        }
    }
}
