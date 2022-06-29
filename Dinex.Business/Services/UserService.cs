
namespace Dinex.Business
{
    public class UserService : IUserService
    {
        const int success = 1;

        private readonly IUserRepository _userRepository;
        private readonly ICryptographyService _cryptographyService;
        private readonly IMapper _mapper;


        public UserService(IUserRepository userRepository, ICryptographyService cryptographyService, IMapper mapper)
        {
            _userRepository = userRepository;
            _cryptographyService = cryptographyService;
            _mapper = mapper;
        }

        private async Task<User> GetFromContextAsync(HttpContext httpContext)
        {
            var user = await (Task<User>)httpContext.Items["User"];
            if (user is null)
            {
                // msg: "User not found"
                throw new AppException(User.Error.UserNotFound.ToString());
            }

            httpContext.Items["User"] = null;

            return user;
        }

        public async Task<UserResponseDto> CreateAsync(UserRequestDto request)
        {
            var user = _mapper.Map<User>(request);

            user.IsActive = UserActivatioStatus.Inactive;
            user.CreatedAt = DateTime.Now;
            user.Password = _cryptographyService.Encrypt(user.Password);

            var result = await _userRepository.AddAsync(user);
            if (result != success)
            {
                // msg : Error to create user
                throw new InfraException(User.Error.ErrorToCreateUser.ToString());
            }

            var userResult = _mapper.Map<UserResponseDto>(user);
            return userResult;
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

        public async Task<UserResponseDto> UpdateAsync(
            UserRequestDto userData,
            bool needUpdatePassword,
            Guid userId)
        {
            var user = await GetByIdAsync(userId);

            user.UpdatedAt = DateTime.Now;

            if (userData.IsActive != null)
                user.IsActive = (UserActivatioStatus)userData.IsActive;

            if (needUpdatePassword)
                user.Password = _cryptographyService.Encrypt(userData.Password);

            var result = await _userRepository.UpdateAsync(user);
            if (result != success)
            {
                // msg: Error to update user
                throw new InfraException(User.Error.ErrorToUpdateUser.ToString());
            }

            var userResult = _mapper.Map<UserResponseDto>(user);
            return userResult;
        }

        public async Task<UserResponseDto> GetUser(HttpContext httpContext)
        {
            var user = await GetFromContextAsync(httpContext);
            if (user is null)
            {
                // msg: "User not found"
                throw new AppException(User.Error.UserNotFound.ToString());
            }

            return _mapper.Map<UserResponseDto>(user);
        }

        #region exclusive for middleware
        public async Task<User> GetByIdAsNoTracking(Guid userId)
        {
            var user = _userRepository.GetByIdAsNoTracking(userId).Result;
            if (user is null)
            {
                // msg: "User not found"
                throw new AppException(User.Error.UserNotFound.ToString());
            }
            return user;
        }
        #endregion
    }
}
