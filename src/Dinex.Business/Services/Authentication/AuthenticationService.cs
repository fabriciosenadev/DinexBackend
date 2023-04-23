namespace Dinex.Business
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly ICryptographyService _cryptographyService;

        public AuthenticationService(
            IUserRepository userRepository,
            IJwtService jwtService,
            ICryptographyService cryptographyService,           
            IMapper mapper,
            INotificationService notification)
            : base(mapper, notification)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _cryptographyService = cryptographyService;
        }

        public async Task<AuthenticationResponseDto> AuthenticateAsync(AuthenticationRequestDto request)
        {
            var login = _mapper.Map<Login>(request);

            var user = await _userRepository.GetByEmailAsync(login.Email);
            if(user is null)
                Notification.RaiseError(Login.Error.LoginNotFound);

            var passwordsMatch = _cryptographyService.CompareValues(user.Password, login.Password);
            if (!passwordsMatch)
                Notification.RaiseError(Login.Error.LoginOrPassIncorrect);

            if (user.IsActive == UserActivatioStatus.Inactive)
                Notification.RaiseError(Login.Error.LoginInactive);

            var token = _jwtService.GenerateToken(user);

            return new AuthenticationResponseDto(user, token);
        }
    }
}
