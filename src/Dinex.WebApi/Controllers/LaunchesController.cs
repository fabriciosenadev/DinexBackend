namespace Dinex.WebApi.API.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("/[controller]")]
    [ApiController]
    public class LaunchesController : ControllerBase
    {
        private readonly ILaunchManager _launchManager;
        private readonly IUserService _userService;        

        public LaunchesController(ILaunchManager launchManager, IUserService userService)
        {
            _launchManager = launchManager;
            _userService = userService;
        }

        private async Task<Guid> GetUserId()
        {
            var user = await _userService.GetUser(HttpContext);
            return user.Id;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<LaunchAndPayMethodRequestDto>> Create([FromBody] LaunchAndPayMethodRequestDto request)
        {
            var userId = await GetUserId();

            var response = await _launchManager.CreateAsync(request, userId);            
            return Ok(response);
        }

        [HttpPut("{Id}")]
        [Authorize]
        public async Task<ActionResult<LaunchAndPayMethodResponseDto>> Update(
            [FromRoute] int id,
            [FromQuery] bool isJustStatus,
            [FromBody] LaunchAndPayMethodRequestDto request)
        {
            var userId = await GetUserId();

            var response = await _launchManager.UpdateAsync(request, id, userId, isJustStatus);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _launchManager.SoftDeleteAsync(id);

            return Ok();
        }

        [HttpGet("{Id}")]
        [Authorize]
        public async Task<ActionResult<LaunchAndPayMethodResponseDto>> Get([FromRoute] int id)
        {
            var response = await _launchManager.GetAsync(id);
            return Ok(response);

        }

        [HttpGet("last")]
        [Authorize]
        public async Task<ActionResult<List<LaunchResponseDto>>> ListLastLaunches()
        {
            var userId = await GetUserId();

            var response = await _launchManager.ListLast(userId);
            return Ok(response);
        }

        [HttpGet("{year}/{month}/resume")]
        [Authorize]
        public async Task<IActionResult> GetResumeByYearAndMonth([FromRoute] int year, [FromRoute] int month)
        {
            var userId = await GetUserId();

            var result = await _launchManager.GetResumeByYearAndMonthAsync(year, month, userId);
            return Ok(result);
        }

        [HttpGet("{year}/{month}/details")]
        [Authorize]
        public async Task<IActionResult> GetDetailsByYearAndMonth([FromRoute] int year, [FromRoute] int month)
        {
            var userId = await GetUserId();

            var result = await _launchManager.GetDetailsByYearAndMonthAsync(year, month, userId);
            return Ok(result);
        }

    }
}
