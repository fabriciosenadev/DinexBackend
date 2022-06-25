using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dinex.WebApi.API.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("v1/[controller]")]
    [ApiController]
    public class LaunchesController : ControllerBase
    {
        private readonly ILaunchService _launchService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public LaunchesController(ILaunchService launchService, IMapper mapper, IUserService userService)
        {
            _launchService = launchService;
            _mapper = mapper;
            _userService = userService;
        }

        private async Task<Guid> GetUserId()
        {
            var user = await _userService.GetFromContextAsync(HttpContext);
            return user.Id;
        }

        private (LaunchRequestModel, PayMethodFromLaunchRequestModel?) GetLaunchAndPayMethodRequest(LaunchAndPayMethodRequestModel model)
        {
            var launch = model.Launch;
            var payMethodFromLaunch = model.PayMethodFromLaunch is not null ? model.PayMethodFromLaunch : null;
            return (launch, payMethodFromLaunch);
        }

        private LaunchAndPayMethodResponseModel GetLaunchAndPayMethodResponse(
            LaunchResponseModel launchResponseModel,
            PayMethodFromLaunchResponseModel? payMethodFromLaunchResponseModel)
        {
            var response = new LaunchAndPayMethodResponseModel
            {
                Launch = launchResponseModel,
                PayMethodFromLaunch = payMethodFromLaunchResponseModel
            };
            return response;
        }

        private List<LaunchResponseModel> FillApplicableToLaunchResponseModel(List<LaunchResponseModel> launchesResponseModel, List<CategoryToUser> categoriesToUser)
        {
            launchesResponseModel.ForEach(launch =>
                {
                    var applicable = categoriesToUser
                        .Where(x => x.CategoryId.Equals(launch.CategoryId))
                        .Select(x => x.Applicable);

                    launch.Applicable = applicable.ElementAt(0).ToString();
                });
            return launchesResponseModel;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<LaunchAndPayMethodRequestModel>> Create([FromBody] LaunchAndPayMethodRequestModel model)
        {
            var userId = await GetUserId();

            var (launchModel, payMethodModel) = GetLaunchAndPayMethodRequest(model);

            var launch = _mapper.Map<Launch>(launchModel);
            launch.UserId = userId;

            PayMethodFromLaunch? payMethod = null;
            if (payMethodModel is not null)
                payMethod = _mapper.Map<PayMethodFromLaunch>(payMethodModel);

            var (launchResult, payMethodResult) = await _launchService.CreateAsync(launch, payMethod);

            if (launchResult is null && payMethodResult is null)
                return BadRequest(new { message = "there was a problem to create launch" });

            PayMethodFromLaunchResponseModel? payMethodFromLaunchResponse = null;
            if (payMethodResult is not null)
                payMethodFromLaunchResponse = _mapper.Map<PayMethodFromLaunchResponseModel>(payMethodResult);

            var launchResponse = _mapper.Map<LaunchResponseModel>(launchResult);

            var response = GetLaunchAndPayMethodResponse(
                launchResponse,
                payMethodFromLaunchResponse);
            return Ok(response);
        }

        [HttpPut("{Id}")]
        [Authorize]
        public async Task<ActionResult<LaunchAndPayMethodResponseModel>> Update(
            [FromRoute] int id,
            [FromBody] LaunchAndPayMethodRequestModel model)
        {
            var userId = await GetUserId();

            var (launchModel, payMethodModel) = GetLaunchAndPayMethodRequest(model);

            var launch = _mapper.Map<Launch>(launchModel);
            launch.Id = id;
            launch.UserId = userId;

            PayMethodFromLaunch? payMethod = null;
            if (payMethodModel is not null)
                payMethod = _mapper.Map<PayMethodFromLaunch>(payMethodModel);

            var (launchResult, payMethodResult) = await _launchService.UpdateAsync(launch, payMethod);

            if (launchResult is null && payMethodResult is null)
                return BadRequest(new { message = "there was a problem to update launch" });

            PayMethodFromLaunchResponseModel? payMethodFromLaunchResponse = null;
            if (payMethodResult is not null)
                payMethodFromLaunchResponse = _mapper.Map<PayMethodFromLaunchResponseModel>(payMethodResult);

            var launchResponse = _mapper.Map<LaunchResponseModel>(launchResult);

            var response = GetLaunchAndPayMethodResponse(
                launchResponse,
                payMethodFromLaunchResponse);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _launchService.SoftDeleteAsync(id);

            if (!result)
                return BadRequest(new { message = "there was a problem to delete launch" });

            return Ok();
        }

        [HttpGet("{Id}")]
        [Authorize]
        public async Task<ActionResult<LaunchAndPayMethodResponseModel>> Get([FromRoute] int id)
        {
            var (launchResult, payMethodResult) = await _launchService.GetAsync(id);

            if (launchResult is null && payMethodResult is null)
                return BadRequest(new { message = "there was a problem to update launch" });

            PayMethodFromLaunchResponseModel? payMethodFromLaunchResponse = null;
            if (payMethodResult is not null)
                payMethodFromLaunchResponse = _mapper.Map<PayMethodFromLaunchResponseModel>(payMethodResult);

            var launchResponse = _mapper.Map<LaunchResponseModel>(launchResult);

            var response = GetLaunchAndPayMethodResponse(
                launchResponse,
                payMethodFromLaunchResponse);
            return Ok(response);

        }

        [HttpGet("last")]
        [Authorize]
        public async Task<ActionResult<List<LaunchResponseModel>>> ListLastLaunches()
        {
            var userId = await GetUserId();

            var (launchesResponse, categoriesToUserResponse) = await _launchService.ListLast(userId);

            var response = _mapper.Map<List<LaunchResponseModel>>(launchesResponse);
            response = FillApplicableToLaunchResponseModel(response, categoriesToUserResponse);
            return Ok(response);
        }

    }
}
