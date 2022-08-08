namespace Dinex.WebApi.API.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IUserAmountAvailableService _userAmountAvailableService;

        public UsersController(
            IUserService userService, 
            IUserAmountAvailableService userAmountAvailableService)
        {
            _userService = userService;
            _userAmountAvailableService = userAmountAvailableService;
        }

        private async Task<Guid> GetUserId()
        {
            var user = await _userService.GetUser(HttpContext);
            return user.Id;
        }


        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> Create([FromBody] UserRequestDto request)
        {
            var result = await _userService.CreateAsync(request);
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserResponseDto>> Get()
        {
            var user = await _userService.GetUser(HttpContext);
            return Ok(user);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<UserResponseDto>> Update([FromBody] UserRequestDto request)
        {
            var userId = await GetUserId();

            var userResult = await _userService
                .UpdateAsync(request, true, userId);

            return Ok(userResult);
        }

        [Authorize]
        [HttpGet("amount-available")]
        public async Task<ActionResult<UserAmountAvailableResponseDto>> GetAmountAvailableAsync()
        {
            var userId = await GetUserId();
            var result = await _userAmountAvailableService.GetAmountAvailableAsync(userId);

            return Ok(result);
        }
    }
}
