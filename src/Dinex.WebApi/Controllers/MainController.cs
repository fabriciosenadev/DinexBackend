namespace Dinex.Backend.WebApi.Controllers;

[EnableCors("_myAllowSpecificOrigins")]
[ApiController]
public abstract class MainController : ControllerBase
{
    public readonly INotificationService _notificationService;
    public MainController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    protected ActionResult SuccessResponse(object? result = null, HttpStatusCode statusCode = HttpStatusCode.OK)
    {

        var resultSuccess = new
        {
            success = true,
            data = result
        };

        return CustomReponse(resultSuccess, statusCode);
    }

    protected ActionResult ErrorResponse(object? result = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        var resultError = new
        {
            success = false,
            data = result
        };

        return CustomReponse(resultError, statusCode);
    }

    protected ActionResult HandleResponse(object? result = null)
    {
        if(_notificationService.HasNotification())
            return ErrorResponse(_notificationService.GetAllNotifications());

        return SuccessResponse(result);
    }

    #region private methods
    private ActionResult CustomReponse(object? result = null, HttpStatusCode statusCode = HttpStatusCode.NoContent)
    {
        switch (statusCode)
        {
            case HttpStatusCode.OK:
                return Ok(result);
            case HttpStatusCode.BadRequest:
                return BadRequest(result);
            case HttpStatusCode.NoContent:
            default:
                return NoContent();
        }
    }
    #endregion
}
