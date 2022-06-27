namespace Dinex.WebApi.API.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("/[controller]")]
    [ApiController]
    public class ActivationsController : ControllerBase
    {
        private readonly IActivationService _activationService;
        public ActivationsController(IActivationService activationService)
        {
            _activationService = activationService;
        }

        [HttpPost("send-code")]
        public async Task<IActionResult> SendActivationCode([FromBody] ActivationRequestDto activation)
        {
            var result = await _activationService.SendActivationCodeAsync(activation.Email);
            return Ok(new { message = result });
        }

        [HttpPost("activate-account")]
        public async Task<IActionResult> ActivateAccount([FromBody] ActivationRequestDto activation)
        {
            await _activationService.ActivateAccountAsync(activation.Email, activation.ActivationCode);

            return Ok();
        }
    }
}
