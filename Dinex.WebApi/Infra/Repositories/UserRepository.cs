
namespace Dinex.WebApi.Infra
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DinexBackendContext context) : base(context)
        {

        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }


        public async Task<User> GetByIdAsNoTracking(Guid userId)
        {
            return  _context.Users.AsNoTracking().FirstOrDefault(u => u.Id.Equals(userId));
        }

        //public async Task<int> UpdateTest(User user)
        //{
        //    _context.Users.Update(user);
        //    return await _context.SaveChangesAsync();
        //}
    }
}
