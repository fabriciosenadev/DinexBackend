
namespace Dinex.WebApi.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptographyService _cryptographyService;

        public UserService(IUserRepository userRepository, ICryptographyService cryptographyService)
        {
            _userRepository = userRepository;
            _cryptographyService = cryptographyService;
        }

        public async Task<int> CreateAsync(User user)
        {
            user.IsActive = UserActivatioStatus.Inactive;
            user.CreatedAt = DateTime.Now;
            user.Password = _cryptographyService.Encrypt(user.Password);

            var result = await _userRepository.AddAsync(user);

            user.Password = null;
            return result;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return user;
        }



        public async Task<User> UpdateAsync(User user, bool needUpdatePassword)
        {
            user.UpdatedAt = DateTime.Now;

            if (needUpdatePassword)
                user.Password = _cryptographyService.Encrypt(user.Password);

            var result = await _userRepository.UpdateAsync(user);

            user.Password = null;
            return user;
        }

        public async Task<User> GetFromContextAsync(HttpContext httpContext)
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
