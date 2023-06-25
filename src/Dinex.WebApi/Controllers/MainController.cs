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

    protected ActionResult SuccessResponse(object? result = null, HttpStatusCode? statusCode = null)
    {

        var resultSuccess = new
        {
            success = true,
            data = result
        };
        statusCode ??= HttpStatusCode.OK;

        return CustomReponse(resultSuccess, statusCode);
    }

    protected ActionResult ErrorResponse(object? result = null, HttpStatusCode? statusCode = null)
    {
        var resultError = new
        {
            success = false,
            data = result
        };

        statusCode ??= HttpStatusCode.BadRequest;

        return CustomReponse(resultError, statusCode);
    }

    protected ActionResult HandleResponse(object? result = null, HttpStatusCode? statusCode = null)
    {
        if(_notificationService.HasNotification())
            return ErrorResponse(_notificationService.GetAllNotifications(), statusCode);

        return SuccessResponse(result);
    }

    #region private methods
    private ActionResult CustomReponse(object? result = null, HttpStatusCode? statusCode = null)
    {
        return statusCode switch
        {
            HttpStatusCode.OK => Ok(result),
            HttpStatusCode.BadRequest => BadRequest(result),
            HttpStatusCode.NoContent => NoContent(),
            _ => NoContent(),
        };
    }
    #endregion
}
