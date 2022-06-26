namespace Dinex.Infra
{
    public class PayMethodFromLaunchRepository : Repository<PayMethodFromLaunch>, IPayMethodFromLaunchRepository
    {
        public PayMethodFromLaunchRepository(DinexBackendContext context) : base(context)
        {
        }

        public async Task<PayMethodFromLaunch> FindRelationAsync(int launchId)
        {
            var result = await _context.PayMethodFromLaunches
                .Where(x => x.LaunchId.Equals(launchId))
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<PayMethodFromLaunch>> ListRelationsAsync(List<int> launchIds)
        {
            var result = await _context.PayMethodFromLaunches
                .Where(c => launchIds.Contains(c.Id))
                .ToListAsync();

            return result;
        }
    }
}
