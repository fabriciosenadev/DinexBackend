namespace Dinex.Backend.WebApi.V1.Controllers;

[Route("/[controller]")]
public class AuthenticationsController : MainController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationsController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }


    [HttpPost]
    public async Task<ActionResult<AuthenticationResponseDto>> Authenticate([FromBody] AuthenticationRequestDto request)
    {
        var response = await _authenticationService.AuthenticateAsync(request);
        return Ok(response);
    }
}
