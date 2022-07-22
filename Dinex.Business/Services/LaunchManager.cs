namespace Dinex.Business
{
    public class LaunchManager : ILaunchManager
    {
        private readonly ILaunchService _launchService;
        private readonly IPayMethodFromLaunchService _payMethodFromLaunchService;
        private readonly ICategoryToUserService _categoryToUserService;
        private readonly ICategoryManager _categoryManager;
        private readonly IMapper _mapper;

        public LaunchManager(
            ILaunchService launchServie,
            IPayMethodFromLaunchService payMethodFromLaunchService,
            ICategoryToUserService categoryToUserService,
            ICategoryManager categoryManager,
            IMapper mapper)
        {
            _launchService = launchServie;
            _payMethodFromLaunchService = payMethodFromLaunchService;
            _categoryToUserService = categoryToUserService;
            _categoryManager = categoryManager;
            _mapper = mapper;
        }

        #region private methods
        private async Task TransferRequestToEntityFieldsAsync(LaunchRequestDto request, Launch launch, bool isJustStatus)
        {
            launch.Date = request.Date;
            launch.Status = isJustStatus ? await GetNewStatus(launch) : launch.Status;
            launch.Amount = request.Amount;
            launch.CategoryId = request.CategoryId;
            launch.Description = request.Description;
        }

        private List<LaunchResponseDto> FillApplicableToLaunchResponseModel(
            List<LaunchResponseDto> launchesResponseModel, List<CategoryToUser> categoriesToUser)
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
            var categoryRelation = await _categoryToUserService
                .GetRelationAsync(launch.CategoryId, launch.UserId);

            if (launch.Status == LaunchStatus.Pending)
            {
                if (categoryRelation.Applicable == Applicable.In)
                    return LaunchStatus.Received;
                else
                    return LaunchStatus.Paid;
            }

            return LaunchStatus.Pending;

        }

        private async Task<List<int>> ListCategoryIdsByApplicable(Applicable applicable, Guid userId)
        {
            var categories = await _categoryManager.ListCategoriesAsync(userId, false);
            var list = categories.Where(x => x.Applicable.Equals(applicable.ToString()))
                .Select(x => x.Id)
                .ToList();

            return list;
        }

        private (DateTime, DateTime) GetStartAndEndDateByYearAndMonth(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            return (startDate, endDate);
        }

        private async Task<List<ChartDataResponseDto>> GetPieChartData(Guid userId, List<Launch> launches)
        {
            var categories = await _categoryManager.ListCategoriesAsync(userId, false);

            List<ChartDataResponseDto> pieChartData = new();
            categories.ForEach(async (category) =>
            {
                var categoryId = category.Id;
                var categoryName = category.Name;
                var applicable = category.Applicable;

                var amount = launches.Where(x => x.CategoryId.Equals(categoryId)).Sum(x => x.Amount);
                var launch = launches.Find(x => x.CategoryId.Equals(categoryId));

                string? payMethodName = string.Empty;

                if (amount > 0)
                {
                    PayMethodFromLaunchResponseDto payMethod;
                    payMethod = await _payMethodFromLaunchService.GetAsync(launch.Id);
                    if (payMethod is not null)
                        payMethodName = payMethod?.PayMethod;

                    var chartData = new ChartDataResponseDto
                    {
                        CategoryId = categoryId,
                        CategoryName = categoryName,
                        Applicable = applicable,
                        Amount = amount,
                        PayMethod = payMethodName
                    };
                    pieChartData.Add(chartData);
                }
            });

            return pieChartData;
        }

        #endregion

        public async Task<LaunchAndPayMethodResponseDto> CreateAsync(LaunchAndPayMethodRequestDto request, Guid userId)
        {
            var (launchModel, payMethodModel) = SplitLaunchAndPayMethodRequests(request);

            var launch = _mapper.Map<Launch>(launchModel);
            var resultLaunchCreation = await _launchService.CreateAsync(launch, userId);
            var launchResponse = _mapper.Map<LaunchResponseDto>(resultLaunchCreation);

            PayMethodFromLaunchResponseDto? payMethodFromLaunchResponse = null;
            if (payMethodModel is not null)
            {
                var payMethodFromLaunch = _mapper.Map<PayMethodFromLaunch>(payMethodModel);
                var payMethodFromLaunchCreation = await _payMethodFromLaunchService
                    .CreateAsync(payMethodFromLaunch, launch.Id);
                payMethodFromLaunchResponse = _mapper.Map<PayMethodFromLaunchResponseDto>(payMethodFromLaunchCreation);
            }

            var response = JoinLaunchAndPayMethodResponses(
                launchResponse,
                payMethodFromLaunchResponse);

            return response;
        }

        public async Task<LaunchAndPayMethodResponseDto> UpdateAsync(
            LaunchAndPayMethodRequestDto request, int launchId, Guid userId, bool isJustStatus)
        {
            var launchStored = await _launchService.GetByIdAsync(launchId);

            var (launchModel, payMethodModel) = SplitLaunchAndPayMethodRequests(request);

            await TransferRequestToEntityFieldsAsync(launchModel, launchStored, isJustStatus);

            await _launchService.UpdateAsync(launchStored);

            var launchResponse = _mapper.Map<LaunchResponseDto>(launchStored);

            PayMethodFromLaunchResponseDto? payMethodFromLaunchResponse = null;
            if (payMethodModel is not null)
                payMethodFromLaunchResponse = await _payMethodFromLaunchService
                    .UpdateAsync(payMethodModel, launchStored.Id);

            var response = JoinLaunchAndPayMethodResponses(
                launchResponse,
                payMethodFromLaunchResponse);

            return response;
        }

        public async Task SoftDeleteAsync(int launchId)
        {
            var launch = await _launchService.GetByIdAsync(launchId);

            await _launchService.SoftDeleteAsync(launch);

            var payMethod = await _payMethodFromLaunchService.GetByLaunchIdWithoutDtoAsync(launchId);
            if (payMethod != null)
                await _payMethodFromLaunchService.SoftDeleteAsync(payMethod);
        }

        public async Task<LaunchAndPayMethodResponseDto> GetAsync(int launchId)
        {
            var launch = await _launchService.GetByIdAsync(launchId);

            var payMethodFromLaunchResponse = await _payMethodFromLaunchService.GetAsync(launchId);

            var launchResponse = _mapper.Map<LaunchResponseDto>(launch);

            var response = JoinLaunchAndPayMethodResponses(
                launchResponse,
                payMethodFromLaunchResponse);

            return response;
        }

        public async Task<List<LaunchResponseDto>> ListLast(Guid userId)
        {
            var launches = await _launchService.ListLast(userId);
            var categoriesToUser = await _categoryToUserService.ListCategoryRelationIdsAsync(userId, false);

            var response = _mapper.Map<List<LaunchResponseDto>>(launches);

            response = FillApplicableToLaunchResponseModel(response, categoriesToUser);
            return response;
        }

        public async Task<LaunchResumeByYearAndMonthResponseDto> GetResumeByYearAndMonthAsync(int year, int month, Guid userId)
        {
            var (startDate, endDate) = GetStartAndEndDateByYearAndMonth(year, month);

            var categoryIdsIn = await ListCategoryIdsByApplicable(Applicable.In, userId);
            var categoryIdsOut = await ListCategoryIdsByApplicable(Applicable.Out, userId);

            var received = await _launchService.GetSumAmountByStatus(
                categoryIdsIn,
                userId,
                LaunchStatus.Received,
                startDate,
                endDate);

            var paid = await _launchService.GetSumAmountByStatus(
                categoryIdsOut,
                userId,
                LaunchStatus.Paid,
                startDate,
                endDate);

            var pendingIn = await _launchService.GetSumAmountByStatus(
                categoryIdsIn,
                userId,
                LaunchStatus.Pending,
                startDate,
                endDate);

            var pendingOut = await _launchService.GetSumAmountByStatus(
                categoryIdsOut,
                userId,
                LaunchStatus.Pending,
                startDate,
                endDate);

            var hasPending = pendingIn > 0 || pendingOut > 0;

            var result = new LaunchResumeByYearAndMonthResponseDto
            {
                Received = received,
                Paid = paid,
                HasPending = hasPending,
                StartDate = startDate,
                EndDate = endDate
            };
            return result;
        }

        public async Task<LaunchDetailsByYearAndMonthResponseDto> GetDetailsByYearAndMonthAsync(int year, int month, Guid userId)
        {
            var (startDate, endDate) = GetStartAndEndDateByYearAndMonth(year, month);

            var launches = await _launchService.ListAsync(startDate, endDate, userId);
            var launchesResponse = _mapper.Map<List<LaunchResponseDto>>(launches);

            var categoriesToUser = await _categoryToUserService.ListCategoryRelationIdsAsync(userId, false);

            launchesResponse = FillApplicableToLaunchResponseModel(launchesResponse, categoriesToUser);

            var pieChartData = await GetPieChartData(userId, launches);

            var result = new LaunchDetailsByYearAndMonthResponseDto
            {
                Launches = launchesResponse,
                PieChartData = pieChartData
            };
            return result;
        }


    }
}
