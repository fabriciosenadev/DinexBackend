namespace Dinex.WebApi.API.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("/[controller]")]
    [ApiController]
    public class ActivationsController : ControllerBase
    {
        private readonly IActivationManager _activationManager;
        public ActivationsController(IActivationManager activationManager)
        {
            _activationManager = activationManager;
        }

        [HttpPost("send-code")]
        public async Task<IActionResult> SendActivationCode([FromBody] ActivationRequestDto activation)
        {
            var result = await _activationManager.SendActivationCodeAsync(activation.Email);
            return Ok(new { message = result });
        }

        [HttpPost("activate-account")]
        public async Task<IActionResult> ActivateAccount([FromBody] ActivationRequestDto activation)
        {
            await _activationManager.ActivateAccountAsync(activation.Email, activation.ActivationCode);

            return Ok();
        }
    }
}
