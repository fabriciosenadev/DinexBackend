namespace Dinex.Infra
{
    public class LaunchRepository : Repository<Launch>, ILaunchRepository
    {
        public LaunchRepository(DinexBackendContext context) : base(context)
        {

        }
        public async Task<Launch> GetByIdAsync(int launchId)
        {
            var result = await _context.Launches
                .Where(x => x.Id.Equals(launchId))
                .FirstOrDefaultAsync();

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
    }
}
