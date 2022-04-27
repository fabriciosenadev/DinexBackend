namespace Dinex.WebApi.API.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper, IUserService userService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _userService = userService;
        }

        private async Task<Guid> GetUserId()
        {
            var user = await _userService.GetFromContext(HttpContext);
            return user.Id;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CategoryResponseModel>> Create([FromBody] CategoryRequestModel model)
        {
            var userId = await GetUserId();
            var category = _mapper.Map<Category>(model);
            
            var resultCreation = await _categoryService.Create(category, userId);            
            
            if (resultCreation is null)
                return BadRequest(new { message = "category already exists"});

            var categoryResult = _mapper.Map<CategoryResponseModel>(resultCreation);

            return Ok(categoryResult);
        }

        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var userId = await GetUserId();

            var result = await _categoryService.Delete(id, userId);
            if (!result)
                return BadRequest(new { message = "something went wrong try later"});

            return Ok();
        }

        [HttpGet("{Id}")]
        [Authorize]
        public async Task<ActionResult<CategoryResponseModel>> Get([FromRoute] int id)
        {
            var userId = await GetUserId();
            var result = await _categoryService.GetCategory(id, userId);
            
            if(result is null)
                return NotFound(new { message = "category not found"});

            var categoryResult = _mapper.Map<CategoryResponseModel>(result);

            return Ok(categoryResult);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<CategoryResponseModel>>> List()
        {
            var userId = await GetUserId();
            var result = await _categoryService.ListCategories(userId);

            if (result is null)
                return new List<CategoryResponseModel>();

            var listResult = _mapper.Map<List<CategoryResponseModel>>(result);

            return listResult;
        }

        [HttpGet("deleted")]
        [Authorize]
        public async Task<ActionResult<List<CategoryResponseModel>>> ListDeleted()
        {
            var userId = await GetUserId();
            var result = await _categoryService.ListCategoriesDeleted(userId);

            if (result is null)
                return new List<CategoryResponseModel>();

            var listResult = _mapper.Map<List<CategoryResponseModel>>(result);

            return listResult;
        }

        [HttpPut("{Id}/reactive")]
        [Authorize]
        public async Task<ActionResult<CategoryResponseModel>> RestoreDeletedCategory([FromRoute] int id)
        {
            var userId = await GetUserId();

            var result = await _categoryService.RestoreDeletedCategory(userId, id);

            if(result is null)
                return BadRequest(new { message = "something went wrong try later" });

            return Ok(result);
        }
    }
}
