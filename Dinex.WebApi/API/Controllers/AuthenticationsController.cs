namespace Dinex.WebApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public AuthenticationsController(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<AuthenticationResponse>> Authenticate(AuthenticationRequest request)
        {
            var login = _mapper.Map<Login>(request);
            var (user, token) = await _authenticationService.Authenticate(login);
            if (user is null)
                return BadRequest(new { message = "User or password is incorrect" });

            var response = new AuthenticationResponse(user, token);
            return Ok(response);
        }
    }
}
