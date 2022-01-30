
namespace Dinex.WebApi.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;


        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> Create(User user)
        {
            user.IsActive = UserActivatioStatus.Inactive;
            user.CreatedAt = DateTime.Now;

            var result = await _userRepository.AddAsync(user);
            
            user.Password = null;

            return result;
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await  _userRepository.GetByIdAsync(id);
            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return user;
        }



        public async Task<User> Update(User user)
        {
            //user.UpdatedAt = DateTime.Now;
            var result = await _userRepository.UpdateAsync(user);

            user.Password = null;
            return user;
        }

        public async Task<User> GetFromContext(HttpContext httpContext)
        {
            return await (Task<User>)httpContext.Items["User"];
        }

        #region exclusive for middleware
        public async Task<User> GetByIdAsNoTracking(Guid userId)
        {
            var user = _userRepository.GetByIdAsNoTracking(userId).Result;
            return user;
        }
        #endregion
    }
}
