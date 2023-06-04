
namespace Dinex.Business
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptographyService _cryptographyService;
        private readonly ICodeManagerService _generationCodeService;

        public UserService(
            IUserRepository userRepository,
            ICryptographyService cryptographyService,
            IMapper mapper,
            INotificationService notification,
            ICodeManagerService generationCodeService)
            : base(mapper, notification)
        {
            _userRepository = userRepository;
            _cryptographyService = cryptographyService;
            _generationCodeService = generationCodeService;
        }

        public async Task<UserResponseDto> CreateAsync(UserRequestDto request)
        {
            User newUser = request;

            newUser.Password = _cryptographyService.Encrypt(newUser.Password);

            var result = await _userRepository.AddUserAsync(newUser);
            if (result != Success)
            {
                Notification.RaiseError(User.Error.UserErrorToCreate);
                return default;
            }

            var userResult = (UserResponseDto)newUser;
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

        public async Task<UserResponseDto> UpdateAsync(User request)
        {
            var user = await GetByIdAsync(request.Id);

            if(user is null)
            {
                Notification.RaiseError(User.Error.UserNotFound);
                return default;
            }

            user.UpdatedAt = DateTime.Now;
            user.Password = _cryptographyService.Encrypt(request.Password);

            var result = await _userRepository.UpdateUserAsync(user);
            if (result != Success)
            {
                Notification.RaiseError(User.Error.UserErrorToCreate);
                return default;
            }

            var userResult = (UserResponseDto)user;
            return userResult;
        }

        public async Task<UserResponseDto> GetUser(HttpContext httpContext)
        {
            var user = await GetFromContextAsync(httpContext);
            if (user is null)
                Notification.RaiseError(User.Error.UserNotFound);

            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task ActivateUserAsync(User user)
        {
            user.IsActive = UserActivatioStatus.Active;
            user.UpdatedAt = DateTime.Now;

            await UpdateAsync(user);
        }

        #region exclusive for middleware
        public async Task<User> GetByIdAsNoTracking(Guid userId)
        {
            var user = _userRepository.GetByIdAsNoTracking(userId).Result;
            if (user is null)
                Notification.RaiseError(User.Error.UserNotFound);

            return user;
        }
        #endregion

        #region Private methods
        private async Task<User?> GetFromContextAsync(HttpContext httpContext)
        {
            var user = await (Task<User>)httpContext.Items["User"];
            if (user is null)
                Notification.RaiseError(User.Error.UserNotFound);

            httpContext.Items["User"] = null;

            return user;
        }
        #endregion
    }
}
