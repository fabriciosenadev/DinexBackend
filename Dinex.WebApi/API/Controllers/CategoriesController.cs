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
            var user = await _userService.GetFromContextAsync(HttpContext);
            return user.Id;
        }

        private List<CategoryResponseModel> FillApplicableToMainList(List<CategoryResponseModel> mainList, List<CategoryToUser> supportList)
        {
            mainList.ForEach(category =>
            {
                var applicable = supportList
                    .Where(x => x.CategoryId == category.Id)
                    .Select(x => x.Applicable);

                category.Applicable = applicable.ElementAt(0).ToString();
            });
            return mainList;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CategoryResponseModel>> Create([FromBody] CategoryRequestModel model)
        {
            var userId = await GetUserId();
            var category = _mapper.Map<Category>(model);
            
            
            var resultCreation = await _categoryService.CreateAsync(category, userId, model.Applicable);                        
            if (resultCreation is null)
                return BadRequest(new { message = "category already exists"});

            var categoryResult = _mapper.Map<CategoryResponseModel>(resultCreation);
            categoryResult.Applicable = model.Applicable;

            return Ok(categoryResult);
        }

        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var userId = await GetUserId();

            var result = await _categoryService.DeleteAsync(id, userId);
            if (!result)
                return BadRequest(new { message = "something went wrong try later"});

            return Ok();
        }

        [HttpGet("{Id}")]
        [Authorize]
        public async Task<ActionResult<CategoryResponseModel>> Get([FromRoute] int id)
        {
            var userId = await GetUserId();
            var result = await _categoryService.GetCategoryAsync(id, userId);
            
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
            var (categoryResult, relationResult) = await _categoryService.ListCategoriesAsync(userId);

            if (categoryResult is null || relationResult is null)
                return new List<CategoryResponseModel>();

            var listResultModel = _mapper.Map<List<CategoryResponseModel>>(categoryResult);

            var listResult = FillApplicableToMainList(listResultModel, relationResult);

            return listResult;
        }

        [HttpGet("deleted")]
        [Authorize]
        public async Task<ActionResult<List<CategoryResponseModel>>> ListDeleted()
        {
            var userId = await GetUserId();
            var (categoryResult, relationResult) = await _categoryService.ListCategoriesDeletedAsync(userId);

            if (categoryResult is null || relationResult is null)
                return new List<CategoryResponseModel>();

            var listResultModel = _mapper.Map<List<CategoryResponseModel>>(categoryResult);

            var listResult = FillApplicableToMainList(listResultModel, relationResult);

            return listResult;
        }

        [HttpPut("{Id}/reactive")]
        [Authorize]
        public async Task<ActionResult<CategoryResponseModel>> RestoreDeletedCategory([FromRoute] int id)
        {
            var userId = await GetUserId();

            var result = await _categoryService.RestoreDeletedCategoryAsync(userId, id);

            if(result is null)
                return BadRequest(new { message = "something went wrong try later" });

            return Ok(result);
        }
    }
}
