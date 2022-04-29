namespace Dinex.WebApi.API.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {

            _userService = userService;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<UserResponseModel>> Create([FromBody] UserRequestModel model)
        {
            const int successCreation = 1;

            var user = _mapper.Map<User>(model);
            var resultCreation = await _userService.CreateAsync(user);

            if (resultCreation != successCreation)
                return BadRequest();

            var userResult = _mapper.Map<UserResponseModel>(user);

            return Ok(userResult);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserResponseModel>> GetById()
        {
            var user = await _userService.GetFromContextAsync(HttpContext);
            if (user is null)
                return BadRequest(new { message = "User not found" });

            var userResult = _mapper.Map<UserResponseModel>(user);

            return Ok(userResult);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<UserResponseModel>> Update([FromBody] UserRequestModel model)
        {
            var user = _mapper.Map<User>(model);

            var httpContextUser = await _userService.GetFromContextAsync(HttpContext);           
            user.Id = httpContextUser.Id;

            var updated = await _userService.UpdateAsync(user, true);
            var userResult = _mapper.Map<UserResponseModel>(updated);

            return Ok(userResult);

        }
    }
}
