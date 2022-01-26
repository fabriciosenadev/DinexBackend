
namespace Dinex.WebApi.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public UserService(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthenticaticationResponse> Authenticate(AuthenticationRequest model)
        {
            var user = await _userRepository.GetByEmailAsync(model.Email);

            if (user is null) return null;

            var token = _jwtService.GenerateToken(user);

            return new AuthenticaticationResponse(user,token);
        }

        public async Task<int> Create(User user)
        {
            user.IsActive = UserActivatioStatus.Inactive;
            user.CreatedAt = DateTime.Now;

            var result = await _userRepository.AddAsync(user);
            
            user.Password = null;

            return result;
        }

        public Task<User> GetById(Guid id)
        {
            var user = _userRepository.GetByIdAsync(id);
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

        public async Task<User> GetByIdAsNoTracking(Guid userId)
        {
            var user = _userRepository.GetByIdAsNoTracking(userId).Result;
            return user;
        }
    }
}
