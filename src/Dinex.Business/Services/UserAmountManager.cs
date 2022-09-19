namespace Dinex.Business
{
    public class UserAmountManager : BaseService, IUserAmountManager
    {
        private readonly IUserAmountAvailableService _userAmountAvailableService;
        private readonly ICategoryToUserRepository _categoryToUserRepository;
        private readonly ILaunchRepository _launchRepository;
        private readonly ILaunchService _launchService;

        public UserAmountManager(
            IMapper mapper,
            INotificationService notificationService,
            IUserAmountAvailableService userAmountAvailableService,
            ICategoryToUserRepository categoryToUserRepository,
            ILaunchRepository launchRepository,
            ILaunchService launchService)
            : base(mapper, notificationService)
        {
            _userAmountAvailableService = userAmountAvailableService;
            _categoryToUserRepository = categoryToUserRepository;
            _launchRepository = launchRepository;
            _launchService = launchService;
        }

        private async Task<UserAmountAvailable> CreateAvailableAmountByUserId(Guid userId)
        {
            var userCategories = await _categoryToUserRepository.ListCategoryRelationIdsAsync(userId);

            var inCategories = userCategories
                .Where(x => x.Applicable.Equals(Applicable.In))
                .Select(x => x.Id).ToList();

            var outCategories = userCategories
                .Where(x => x.Applicable.Equals(Applicable.Out))
                .Select(x => x.Id).ToList();

            var inLaunchesValue = await _launchService.GetLaunchesSumByCategoriesId(userId, inCategories);
            var outLaunchesValue = await _launchService.GetLaunchesSumByCategoriesId(userId, outCategories);

            var availableValue = inLaunchesValue - outLaunchesValue;

            var amountAvailable = new UserAmountAvailable
            {
                AmountAvailable = availableValue,
                UserId = userId,
            };

            await _userAmountAvailableService.CreateAsync(amountAvailable);

            return amountAvailable;
        }

        public async Task<UserAmountAvailableResponseDto> GetAmountAvailableByUserId(Guid userId)
        {
            var amountAvailable = await _userAmountAvailableService.GetAmountAvailableAsync(userId);

            if (amountAvailable is null)
               amountAvailable = await CreateAvailableAmountByUserId(userId);

            var result = _mapper.Map<UserAmountAvailableResponseDto>(amountAvailable);
            return result;
        }
    }
}
