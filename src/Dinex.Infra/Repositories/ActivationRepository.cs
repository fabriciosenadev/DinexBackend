namespace Dinex.Infra
{
    public class ActivationRepository : Repository<Activation>, IActivationRepository
    {
        public ActivationRepository(DinexBackendContext context) : base(context)
        {

        }

        public async Task DeleteByUserIdAsync(Guid userId)
        {
            _context.Activations.RemoveRange(_context.Activations.Where(a => a.UserId.Equals(userId)));
            await _context.SaveChangesAsync();
        }

        public async Task<List<Activation>> ListByUserIdAsync(Guid userId)
        {
            return await _context.Activations.Where(a => a.UserId.Equals(userId)).ToListAsync();
        }
    }
}
