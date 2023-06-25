namespace Dinex.Backend.WebApi.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class UploadHistoryFileController : MainController
    {
        private readonly IUserService _userService;
        private readonly IHistoryFileManager _historyFileManager;

        public UploadHistoryFileController(INotificationService notificationService,
            IUserService userService,
            IHistoryFileManager historyFileManager)
            : base(notificationService)
        {
            _userService = userService;
            _historyFileManager = historyFileManager;
        }

        private async Task<Guid> GetUserId()
        {
            var user = await _userService.GetUser(HttpContext);
            return user.Id;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ReceiveHistoryFile([FromForm] HistoryFileRequestDto request)
        {
            var userId = await GetUserId();
            var result = await _historyFileManager.ReceiveHistoryFile(request, userId);
            return HandleResponse(result);
        }
    }
}
