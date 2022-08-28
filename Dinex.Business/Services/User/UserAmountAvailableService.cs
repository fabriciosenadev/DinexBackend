namespace Dinex.Business
{
    public class UserAmountAvailableService : BaseService, IUserAmountAvailableService
    {
        private readonly IUserAmountAvailableRepository _userAmountAvailableRepository;
        public UserAmountAvailableService(
            IMapper mapper,
            INotificationService notificationService,
            IUserAmountAvailableRepository userAmountAvailableRepository) : base(mapper, notificationService)
        {
            _userAmountAvailableRepository = userAmountAvailableRepository;
        }

        public async Task<UserAmountAvailable> GetAmountAvailableAsync(Guid userId)
        {
            var result = await _userAmountAvailableRepository.GetAmountAvailableAsync(userId);
            return result;
        }

        public async Task<UserAmountAvailable> CreateAsync(UserAmountAvailable userAmountAvailable)
        {
            var result = await _userAmountAvailableRepository.AddAsync(userAmountAvailable);
            if (result != Success)
                Notification.RaiseError(
                    UserAmountAvailable.Error.UserAmountErrorToCreate,
                    NotificationService.ErrorType.Infra);


            return userAmountAvailable;
        }

        public async Task UpdateAsync(UserAmountAvailable userAmountAvailable)
        {
            await _userAmountAvailableRepository.UpdateAsync(userAmountAvailable);
        }
    }
}
