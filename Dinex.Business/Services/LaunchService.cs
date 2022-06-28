namespace Dinex.Business
{
    public class LaunchService : ILaunchService
    {
        private readonly ILaunchRepository _launchRepository;
        private readonly IPayMethodFromLaunchService _payMethodFromLaunchService;
        private readonly ICategoryToUserService _categoryToUserService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public LaunchService(
            ILaunchRepository launchRepository,
            IPayMethodFromLaunchService payMethodFromLaunchService,
            ICategoryToUserService categoryToUserService,
            IMapper mapper,
            ICategoryService categoryService)
        {
            _launchRepository = launchRepository;
            _payMethodFromLaunchService = payMethodFromLaunchService;
            _categoryToUserService = categoryToUserService;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        private List<LaunchResponseDto> FillApplicableToLaunchResponseModel(List<LaunchResponseDto> launchesResponseModel, List<CategoryToUser> categoriesToUser)
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

        private (LaunchRequestDto, PayMethodFromLaunchRequestDto?) SplitLaunchAndPayMethodRequests(LaunchAndPayMethodRequestDto model)
        {
            var launch = model.Launch;
            var payMethodFromLaunch = model.PayMethodFromLaunch is not null ? model.PayMethodFromLaunch : null;
            return (launch, payMethodFromLaunch);
        }

        private LaunchAndPayMethodResponseDto JoinLaunchAndPayMethodResponses(
            LaunchResponseDto launchResponseModel,
            PayMethodFromLaunchResponseDto? payMethodFromLaunchResponseModel)
        {
            var response = new LaunchAndPayMethodResponseDto
            {
                Launch = launchResponseModel,
                PayMethodFromLaunch = payMethodFromLaunchResponseModel
            };
            return response;
        }

        private async Task<LaunchStatus> GetNewStatus(Launch launch)
        {
            var category = await _categoryService.GetCategoryAsync(launch.CategoryId, launch.UserId);
            if (launch.Status == LaunchStatus.Pending)
            {
                if (category.Applicable == Applicable.In.ToString())
                    return LaunchStatus.Paid;
                else
                    return LaunchStatus.Received;
            }

            return LaunchStatus.Pending;
        }

        public async Task<LaunchAndPayMethodResponseDto> CreateAsync(LaunchAndPayMethodRequestDto request, Guid userId)
        {
            var (launchModel, payMethodModel) = SplitLaunchAndPayMethodRequests(request);

            var launch = _mapper.Map<Launch>(launchModel);
            launch.UserId = userId;
            launch.CreatedAt = DateTime.Now;
            launch.UpdatedAt = launch.DeletedAt = null;

            var launchResult = await _launchRepository.AddAsync(launch);
            if (launchResult != 1)
                throw new AppException("there was a problem to create launch");

            var launchResponse = _mapper.Map<LaunchResponseDto>(launch);

            PayMethodFromLaunchResponseDto? payMethodFromLaunchResponse = null;
            if (payMethodModel is not null)
                payMethodFromLaunchResponse = await _payMethodFromLaunchService.CreateAsync(payMethodModel, launch.Id);


            var response = JoinLaunchAndPayMethodResponses(
                launchResponse,
                payMethodFromLaunchResponse);

            return response;
        }

        public async Task<LaunchAndPayMethodResponseDto> UpdateAsync(LaunchAndPayMethodRequestDto request, int launchId, Guid userId, bool isJustStatus)
        {
            var (launchModel, payMethodModel) = SplitLaunchAndPayMethodRequests(request);

            var launch = _mapper.Map<Launch>(launchModel);
            launch.Id = launchId;
            launch.UserId = userId;
            launch.UpdatedAt = DateTime.Now;

            if (isJustStatus)
                launch.Status = await GetNewStatus(launch);

            var launchResult = await _launchRepository.UpdateAsync(launch);

            if (launchResult != 1)
                throw new AppException("there was a problem to update launch");

            var launchResponse = _mapper.Map<LaunchResponseDto>(launch);

            PayMethodFromLaunchResponseDto? payMethodFromLaunchResponse = null;
            if (payMethodModel is not null)
            {
                payMethodFromLaunchResponse = await _payMethodFromLaunchService.UpdateAsync(payMethodModel, launch.Id);
            }

            var response = JoinLaunchAndPayMethodResponses(
                launchResponse,
                payMethodFromLaunchResponse);

            return response;
        }

        public async Task SoftDeleteAsync(int launchId)
        {
            var launch = await _launchRepository.GetByIdAsync(launchId);
            if (launch is null)
                throw new AppException("Launch not found");

            launch.DeletedAt = DateTime.Now;

            var result = await _launchRepository.UpdateAsync(launch);
            if (result != 1)
                throw new AppException("there was a problem to delete launch");

            var payMethod = await _payMethodFromLaunchService.GetByLaunchIdWithoutDtoAsync(launchId);
            if (payMethod != null)
                await _payMethodFromLaunchService.SoftDeleteAsync(payMethod);
        }

        public async Task<LaunchAndPayMethodResponseDto> GetAsync(int launchId)
        {
            var launch = await _launchRepository.GetByIdAsync(launchId);

            if (launch is null)
                throw new AppException("Launch not found");

            var payMethodFromLaunchResponse = await _payMethodFromLaunchService.GetAsync(launchId);

            var launchResponse = _mapper.Map<LaunchResponseDto>(launch);

            var response = JoinLaunchAndPayMethodResponses(
                launchResponse,
                payMethodFromLaunchResponse);

            return response;
        }

        public Task<List<LaunchAndPayMethodResponseDto>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LaunchResponseDto>> ListLast(Guid userId)
        {
            var launches = await _launchRepository.ListLast(userId);
            var categoriesToUser = await _categoryToUserService.ListCategoryRelationIdsAsync(userId);

            var response = _mapper.Map<List<LaunchResponseDto>>(launches);
            response = FillApplicableToLaunchResponseModel(response, categoriesToUser);

            return response;
        }
    }
}
