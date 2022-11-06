
namespace Dinex.Business
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptographyService _cryptographyService;
        private readonly IGenerationCodeService _generationCodeService;

        public UserService(
            IUserRepository userRepository,
            ICryptographyService cryptographyService,
            IMapper mapper,
            INotificationService notification,
            IGenerationCodeService generationCodeService)
            : base(mapper, notification)
        {
            _userRepository = userRepository;
            _cryptographyService = cryptographyService;
            _generationCodeService = generationCodeService;
        }

        #region Private methods
        private async Task<User> GetFromContextAsync(HttpContext httpContext)
        {
            var user = await (Task<User>)httpContext.Items["User"];
            if (user is null)
                Notification.RaiseError(User.Error.UserNotFound);

            httpContext.Items["User"] = null;

            return user;
        }
        #endregion

        #region exclusive for middleware
        public async Task<User> GetByIdAsNoTracking(Guid userId)
        {
            var user = _userRepository.GetByIdAsNoTracking(userId).Result;
            if (user is null)
                Notification.RaiseError(User.Error.UserNotFound);

            return user;
        }
        #endregion

        public async Task<UserResponseDto> CreateAsync(UserRequestDto request)
        {
            var user = _mapper.Map<User>(request);

            user.IsActive = UserActivatioStatus.Inactive;
            user.CreatedAt = DateTime.Now;
            user.Password = _cryptographyService.Encrypt(user.Password);

            var result = await _userRepository.AddAsync(user);
            if (result != Success)
            {
                Notification.RaiseError(
                    User.Error.UserErrorToCreate, 
                    NotificationService.ErrorType.Infra);
                return default;
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
            UserRequestDto request,
            bool needUpdatePassword)
        {
            Guid userId = Guid.NewGuid();
            if(request.Id.HasValue)
                userId = request.Id.Value;

            var user = await GetByIdAsync(userId);

            user.UpdatedAt = DateTime.Now;

            if (request.IsActive != null)
                user.IsActive = request.IsActive.Value;

            if (needUpdatePassword)
                user.Password = _cryptographyService.Encrypt(request.Password);

            var result = await _userRepository.UpdateAsync(user);
            if (result != Success)
                Notification.RaiseError(
                    User.Error.UserErrorToCreate, 
                    NotificationService.ErrorType.Infra);

            var userResult = _mapper.Map<UserResponseDto>(user);
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

            var userDto = _mapper.Map<UserRequestDto>(user);

            await UpdateAsync(userDto, false);
        }


    }
}
