﻿namespace Dinex.WebApi.API.Controllers
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
            var reason = await _activationService.ActivateAccountAsync(activation.Email, activation.ActivationCode);

            string message = null;
            switch (reason)
            {
                case ActivationReason.ExpiredCode:
                    message = "activation code was expired";
                    break;
                case ActivationReason.InvalidCode:
                    message = "activation code was invalid";
                    break;
            }

            if (!string.IsNullOrEmpty(message))
                return BadRequest(message);

            return Ok();
        }
    }
}
