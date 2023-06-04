using Dinex.Core;

namespace Dinex.Backend.WebApi.V1.Controllers;

[Route("/[controller]")]
public class UsersController : MainController
{

    private readonly IUserService _userService;
    private readonly IUserAmountManager _userAmountManager;

    public UsersController(
        IUserService userService,
        IUserAmountManager userAmountManager, 
        INotificationService notificationService) : base(notificationService)
    {
        _userService = userService;
        _userAmountManager = userAmountManager;
    }

    private async Task<Guid> GetUserId()
    {
        var user = await _userService.GetUser(HttpContext);
        return user.Id;
    }

    #region Unauthenticated routes
    [HttpPost]
    public async Task<ActionResult<UserResponseDto>> Create([FromBody] UserRequestDto request)
    {
        var result = await _userService.CreateAsync(request);
        return HandleResponse(null);
    }

    [HttpPost("send-reset-code")]
    public async Task<IActionResult> SendResetPasswordCode([FromBody] UserResetPasswordDto request)
    {
        //TODO: need to implement service
        return HandleResponse(null);
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] UserResetPasswordDto request)
    {
        //TODO: need to implement service
        return HandleResponse(null);
    }
    #endregion

    #region Authenticated routes
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserResponseDto>> Get()
    {
        var user = await _userService.GetUser(HttpContext);
        return HandleResponse(user);
    }

    [Authorize]
    [HttpPut("{userId}")]
    public async Task<ActionResult<UserResponseDto>> Update([FromRoute] string userId, [FromBody] UserRequestDto request)
    {
        //request.Id = await GetUserId();
        request.Id = new Guid(userId.ToUpper());

        var userResult = await _userService
            .UpdateAsync(request);

        return SuccessResponse(null, HttpStatusCode.NoContent);
    }

    [Authorize]
    [HttpGet("amount-available")]
    public async Task<ActionResult<UserAmountAvailableResponseDto>> GetAmountAvailableAsync()
    {
        var userId = await GetUserId();
        var result = await _userAmountManager.GetAmountAvailableByUserId(userId);

        return HandleResponse(result);
    }
    #endregion
}
