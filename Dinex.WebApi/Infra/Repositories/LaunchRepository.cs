namespace Dinex.WebApi.Infra
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
    }
}
