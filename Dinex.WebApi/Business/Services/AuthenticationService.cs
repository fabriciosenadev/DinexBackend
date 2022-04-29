namespace Dinex.WebApi.Business
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly ICryptographyService _cryptographyService;

        public AuthenticationService(
            IUserRepository userRepository,
            IJwtService jwtService,
            ICryptographyService cryptographyService
            )
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _cryptographyService = cryptographyService;
        }

        public async Task<(User, string)> AuthenticateAsync(Login loginData)
        {
            var user = await _userRepository.GetByEmailAsync(loginData.Email);

            var passwordsMatch = _cryptographyService.CompareValues(user.Password, loginData.Password);

            if (user is null || !passwordsMatch)
                return (null, null);

            if (user.IsActive == UserActivatioStatus.Inactive)
                return (user, null);

            var token = _jwtService.GenerateToken(user);

            return (user, token);
        }
    }
}
