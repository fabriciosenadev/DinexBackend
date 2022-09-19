namespace Dinex.WebApi.API.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;
        private readonly IUserService _userService;


        public CategoriesController(ICategoryManager categoryManager, IUserService userService)
        {
            _categoryManager = categoryManager;
            _userService = userService;
        }

        private async Task<Guid> GetUserId()
        {
            var user = await _userService.GetUser(HttpContext);
            return user.Id;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CategoryResponseDto>> Create([FromBody] CategoryRequestDto request)
        {
            var userId = await GetUserId();
            
            var resultCreation = await _categoryManager
                .CreateAsync(request, userId, request.Applicable);

            return Ok(resultCreation);
        }

        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var userId = await GetUserId();

            await _categoryManager.DeleteAsync(id, userId);

            return Ok();
        }

        [HttpGet("{Id}")]
        [Authorize]
        public async Task<ActionResult<CategoryResponseDto>> Get([FromRoute] int id)
        {
            var userId = await GetUserId();

            var result = await _categoryManager.GetCategoryAsync(id, userId);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<CategoryResponseDto>>> List()
        {
            var userId = await GetUserId();

            var listResult = await _categoryManager.ListCategoriesAsync(userId, false);
            return listResult;
        }

        [HttpGet("deleted")]
        [Authorize]
        public async Task<ActionResult<List<CategoryResponseDto>>> ListDeleted()
        {
            var userId = await GetUserId();

            var listResult = await _categoryManager.ListCategoriesAsync(userId, true);
            return listResult;
        }

        [HttpPut("{Id}/re-activate")]
        [Authorize]
        public async Task<ActionResult<CategoryResponseDto>> RestoreDeletedCategory([FromRoute] int id)
        {
            var userId = await GetUserId();

            var result = await _categoryManager.RestoreDeletedCategoryAsync(userId, id);
            return Ok(result);
        }
    }
}
