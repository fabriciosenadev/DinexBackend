namespace Dinex.WebApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivationsController : ControllerBase
    {
        private readonly IActivationService _activationService;
        public ActivationsController(IActivationService activationService)
        {
            _activationService = activationService;
        }

        [HttpPost("send-code")]
        public async Task<IActionResult> SendActivationCode(ActivationInputModel activation)
        {
            var result = await _activationService.SendActivationCode(activation.Email);
            return Ok(new { message = result });
        }

        [HttpPost("activate-account")]
        public async Task ActivateAccount(ActivationInputModel activation)
        {
            await _activationService.ActivateAccount(activation.Email, activation.ActivationCode);
        }
    }
}
