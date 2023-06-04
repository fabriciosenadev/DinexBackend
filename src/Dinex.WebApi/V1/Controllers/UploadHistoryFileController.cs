namespace Dinex.Backend.WebApi.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class UploadHistoryFileController : MainController
    {
        public UploadHistoryFileController(INotificationService notificationService) 
            : base(notificationService)
        {
        }

        [HttpPost]
        public async Task<ActionResult> ReceiveHistoryFile(IFormFile file)
        {
            return Ok(file);
        }
    }
}
