namespace Dinex.WebApi.API.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("v1/[controller]")]
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
        public async Task<ActionResult<AuthenticationResponseModel>> Authenticate(AuthenticationRequestModel request)
        {
            var login = _mapper.Map<Login>(request);
            var (user, token) = await _authenticationService.Authenticate(login);
            if (user is null && token is null)
                return BadRequest(new { message = "Usuario ou senha incorreto(s)" });

            if (user is not null && token is null)
                return BadRequest(new { message = "Usuario inativo, ative seu acesso" });

            var response = new AuthenticationResponseModel(user, token);
            return Ok(response);
        }
    }
}
