namespace Dinex.WebApi.API.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("/[controller]")]
    [ApiController]
    public class LaunchesController : ControllerBase
    {
        private readonly ILaunchService _launchService;
        private readonly IUserService _userService;        

        public LaunchesController(ILaunchService launchService, IUserService userService)
        {
            _launchService = launchService;
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

            var response = await _launchService.CreateAsync(request, userId);            
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

            var response = await _launchService.UpdateAsync(request, id, userId, isJustStatus);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _launchService.SoftDeleteAsync(id);

            return Ok();
        }

        [HttpGet("{Id}")]
        [Authorize]
        public async Task<ActionResult<LaunchAndPayMethodResponseDto>> Get([FromRoute] int id)
        {
            var response = await _launchService.GetAsync(id);
            return Ok(response);

        }

        [HttpGet("last")]
        [Authorize]
        public async Task<ActionResult<List<LaunchResponseDto>>> ListLastLaunches()
        {
            var userId = await GetUserId();

            var response = await _launchService.ListLast(userId);
            return Ok(response);
        }

    }
}
