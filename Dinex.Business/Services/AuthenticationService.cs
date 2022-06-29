namespace Dinex.Business
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly ICryptographyService _cryptographyService;
        private readonly IMapper _mapper;

        public AuthenticationService(
            IUserRepository userRepository,
            IJwtService jwtService,
            ICryptographyService cryptographyService
,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _cryptographyService = cryptographyService;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponseDto> AuthenticateAsync(AuthenticationRequestDto request)
        {
            var login = _mapper.Map<Login>(request);

            var user = await _userRepository.GetByEmailAsync(login.Email);
            if(user is null)
            {
                // msg: Usuário não localizado
                throw new AppException(Login.Error.LoginNotFound.ToString());
            }

            var passwordsMatch = _cryptographyService.CompareValues(user.Password, login.Password);
            if (!passwordsMatch)
            {
                // msg: Usuário ou senha incorreto
                throw new AppException(Login.Error.LoginOrPassIncorrect.ToString());
            }

            if (user.IsActive == UserActivatioStatus.Inactive)
            {
                // msg: Ative sua conta
                throw new AppException(Login.Error.LoginInactive.ToString());
            }

            var token = _jwtService.GenerateToken(user);

            return new AuthenticationResponseDto(user, token);
        }
    }
}
