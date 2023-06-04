
namespace Dinex.Infra
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IRepository<User> _repository;

        public UserRepository(DinexBackendContext context, 
            IRepository<User> repository) : base(context)
        {
            _repository = repository;
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

        public async Task<int> AddUserAsync(User user)
        {
            return await _repository.AddAsync(user);
        }

        public async Task<int> UpdateUserAsync(User user)
        {
            return await _repository.UpdateAsync(user);
        }
    }
}
