namespace Dinex.Backend.WebApi.V1.Controllers;

[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class AccountsController : MainController
{
    private readonly IActivationAccountManager _activationManager;
    public AccountsController(INotificationService notificationService, 
        IActivationAccountManager activationManager) 
        : base(notificationService)
    {
        _activationManager = activationManager;
    }

    [HttpPost("send-code")]
    public async Task<IActionResult> SendActivationCode([FromBody] AccountActivationRequestDto requestActivation)
    {
        var result = await _activationManager.SendActivationCodeAsync(requestActivation.Email);
        return HandleResponse(result);
    }

    [HttpPost("activate")]
    public async Task<IActionResult> Activate([FromBody] AccountActivationRequestDto requestActivation)
    {
        await _activationManager.ActivateAccountAsync(requestActivation.Email, requestActivation.ActivationCode);

        return HandleResponse(null);
    }

    [HttpPost("send-reset-code")]
    public async Task<IActionResult> SendResetPasswordCode([FromBody] AccountResetPasswordRequestDto request)
    {
        //TODO: need to implement service
        return HandleResponse(null);
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] AccountResetPasswordRequestDto request)
    {
        //TODO: need to implement service
        return HandleResponse(null);
    }
}
