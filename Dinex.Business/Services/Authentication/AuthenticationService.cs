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
            {
                // msg: Usuário não localizado
                Notification.AppRaiseError(Login.Error.LoginNotFound);
            }

            var passwordsMatch = _cryptographyService.CompareValues(user.Password, login.Password);
            if (!passwordsMatch)
            {
                // msg: Usuário ou senha incorreto
                Notification.AppRaiseError(Login.Error.LoginOrPassIncorrect);
            }

            if (user.IsActive == UserActivatioStatus.Inactive)
            {
                // msg: Ative sua conta
                Notification.AppRaiseError(Login.Error.LoginInactive);
            }

            var token = _jwtService.GenerateToken(user);

            return new AuthenticationResponseDto(user, token);
        }
    }
}
