namespace Dinex.Infra
{
    public class UserAmountAvailableRepository : Repository<UserAmountAvailable>, IUserAmountAvailableRepository
    {
        public UserAmountAvailableRepository(DinexBackendContext context) : base(context)
        {

        }

        public async Task<UserAmountAvailable> GetAmountAvailableAsync(Guid userId)
        {
            return await _context.UserAmountAvailable
                .FirstOrDefaultAsync(x => x.UserId.Equals(userId));
        }
    }
}
