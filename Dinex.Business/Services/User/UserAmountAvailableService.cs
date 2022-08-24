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

        public async Task<UserAmountAvailableResponseDto> GetAmountAvailableAsync(Guid userId)
        {
            var result = await _userAmountAvailableRepository.GetAmountAvailableAsync(userId);

            var amountAvailable = _mapper.Map<UserAmountAvailableResponseDto>(result);

            if (amountAvailable is null)
            {
                amountAvailable = new UserAmountAvailableResponseDto
                {
                    AmountAvailable = 0
                };
            }

            return amountAvailable;
        }

        public async Task<UserAmountAvailable> CreateAsync(UserAmountAvailable userAmountAvailable)
        {
            var result = await _userAmountAvailableRepository.UpdateAsync(userAmountAvailable);
            if (result != Success)
                Notification.RaiseError(
                    UserAmountAvailable.Error.ErrorToCreateUserAmount, 
                    NotificationService.ErrorType.Infra);


            return userAmountAvailable;
        }

        public async Task UpdateAsync(UserAmountAvailable userAmountAvailable)
        {
            await _userAmountAvailableRepository.UpdateAsync(userAmountAvailable);
        }
    }
}
