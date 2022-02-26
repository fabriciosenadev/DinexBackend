namespace Dinex.WebApi.Business
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        public AuthenticationService(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<(User, string)> Authenticate(Login loginData)
        {
            var user = await _userRepository.GetByEmailAsync(loginData.Email);

            if (user is null || user.Password != loginData.Password) 
                return (null, null);
            
            if(user.IsActive == UserActivatioStatus.Inactive) 
                return (user, null);

            var token = _jwtService.GenerateToken(user);

            return (user, token);
        }
    }
}
