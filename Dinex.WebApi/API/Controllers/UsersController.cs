namespace Dinex.WebApi.API.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult<UserSearchResult>> Create(UserInputModel model)
        {
            const int successCreation = 1;

            var user = _mapper.Map<User>(model);
            var resultCreation = await _userService.Create(user);

            if (resultCreation != successCreation)
                return BadRequest();

            var userResult = _mapper.Map<UserSearchResult>(user);

            return Ok(userResult);
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticaticationResponse>> Authenticate(AuthenticationRequest request)
        {
            var response = await _userService.Authenticate(request);
            if (response is null)
                return BadRequest(new { message = "User or password is incorrect" });

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserSearchResult>> GetById()
        {
            var user = await _userService.GetFromContext(HttpContext);
            if (user is null)
                return BadRequest(new { message = "User not found" });

            var userResult = _mapper.Map<UserSearchResult>(user);

            return Ok(userResult);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<UserSearchResult>> Update(UserInputModel model)
        {
            //var user = new User();
            //user.FullName = model.FullName;
            //user.Email = model.Email;
            //user.UpdatedAt = DateTime.Now;
            //user.Password = model.Password;

            var user = _mapper.Map<User>(model);

            var httpContextUser = await _userService.GetFromContext(HttpContext);           
            user.Id = httpContextUser.Id;

            var updated = await _userService.Update(user);
            var userResult = _mapper.Map<UserSearchResult>(updated);

            return Ok(userResult);

        }
    }
}
