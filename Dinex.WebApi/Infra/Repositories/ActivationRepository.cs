namespace Dinex.WebApi.Infra
{
    public class ActivationRepository : Repository<Activation>, IActivationRepository
    {
        public ActivationRepository(DinexBackendContext context) : base(context)
        {

        }

        public async Task<List<Activation>> ListByUserIdAsync(Guid userId)
        {
            return await _context.Activations.Where(a => a.UserId.Equals(userId)).ToListAsync();
        }
    }
}
