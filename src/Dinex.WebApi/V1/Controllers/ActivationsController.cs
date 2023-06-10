﻿namespace Dinex.Backend.WebApi.V1.Controllers;

[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class ActivationsController : MainController
{
    private readonly IActivationAccountManager _activationManager;
    public ActivationsController(IActivationAccountManager activationManager, 
        INotificationService notificationService)
        : base(notificationService)
    {
        _activationManager = activationManager;
    }

    [HttpPost("send-code")]
    public async Task<IActionResult> SendActivationCode([FromBody] ActivationRequestDto activation)
    {
        var result = await _activationManager.SendActivationCodeAsync(activation.Email);
        return SuccessResponse(new { message = result });
    }

    [HttpPost("activate-account")]
    public async Task<IActionResult> ActivateAccount([FromBody] ActivationRequestDto activation)
    {
        await _activationManager.ActivateAccountAsync(activation.Email, activation.ActivationCode);

        return SuccessResponse(HttpStatusCode.NoContent);
    }
}
