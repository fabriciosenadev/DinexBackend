namespace Dinex.Backend.WebApi.V1.Controllers;

[Route("/[controller]")]
public class LaunchesController : MainController
{
    private readonly ILaunchManager _launchManager;
    private readonly IUserService _userService;

    public LaunchesController(ILaunchManager launchManager, 
        IUserService userService, 
        INotificationService notificationService) 
        : base(notificationService)
    {
        _launchManager = launchManager;
        _userService = userService;
    }

    private async Task<Guid> GetUserId()
    {
        var user = await _userService.GetUser(HttpContext);
        return user.Id;
    }

    //[HttpPost]
    //[Authorize]
    //public async Task<ActionResult<LaunchAndPayMethodRequestDto>> Create([FromBody] LaunchAndPayMethodRequestDto request)
    //{
    //    var userId = await GetUserId();

    //    var response = await _launchManager.CreateAsync(request, userId);
    //    return SuccessResponse(response);
    //}

    //[HttpPut("{Id}")]
    //[Authorize]
    //public async Task<ActionResult<LaunchAndPayMethodResponseDto>> Update(
    //    [FromRoute] int id,
    //    [FromQuery] bool isJustStatus,
    //    [FromBody] LaunchAndPayMethodRequestDto request)
    //{
    //    var userId = await GetUserId();

    //    var response = await _launchManager.UpdateAsync(request, id, userId, isJustStatus);
    //    return SuccessResponse(response);
    //}

    //[HttpDelete("{Id}")]
    //[Authorize]
    //public async Task<IActionResult> Delete(int id)
    //{
    //    await _launchManager.SoftDeleteAsync(id);

    //    return SuccessResponse(HttpStatusCode.NoContent);
    //}

    //[HttpGet("{Id}")]
    //[Authorize]
    //public async Task<ActionResult<LaunchAndPayMethodResponseDto>> Get([FromRoute] int id)
    //{
    //    var response = await _launchManager.GetAsync(id);
    //    return SuccessResponse(response);

    //}

    //[HttpGet("last")]
    //[Authorize]
    //public async Task<ActionResult<List<LaunchResponseDto>>> ListLastLaunches()
    //{
    //    var userId = await GetUserId();

    //    var response = await _launchManager.ListLast(userId);
    //    return SuccessResponse(response);
    //}

    //[HttpGet("{year}/{month}/resume")]
    //[Authorize]
    //public async Task<IActionResult> GetResumeByYearAndMonth([FromRoute] int year, [FromRoute] int month)
    //{
    //    var userId = await GetUserId();

    //    var result = await _launchManager.GetResumeByYearAndMonthAsync(year, month, userId);
    //    return SuccessResponse(result);
    //}

    //[HttpGet("{year}/{month}/details")]
    //[Authorize]
    //public async Task<IActionResult> GetDetailsByYearAndMonth([FromRoute] int year, [FromRoute] int month)
    //{
    //    var userId = await GetUserId();

    //    var result = await _launchManager.GetDetailsByYearAndMonthAsync(year, month, userId);
    //    return SuccessResponse(result);
    //}

}
