namespace Dinex.Backend.WebApi.Controllers;

[EnableCors("_myAllowSpecificOrigins")]
[ApiController]
public class MainController : ControllerBase
{
    protected ActionResult SuccessResponse(object? result = null, HttpStatusCode statusCode = HttpStatusCode.OK)
    {

        var resultSuccess = new 
        { 
            success = true,
            data = result
        };

        switch(statusCode)
        {                    
            case HttpStatusCode.NoContent:
                return NoContent();
            default:
                return Ok(resultSuccess);
        }
    }
}
