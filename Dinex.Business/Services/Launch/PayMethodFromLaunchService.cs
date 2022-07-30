namespace Dinex.Business
{
    public class PayMethodFromLaunchService : BaseService, IPayMethodFromLaunchService
    {
        private readonly IPayMethodFromLaunchRepository _payMethodFromLaunchRepository;

        public PayMethodFromLaunchService(
            IPayMethodFromLaunchRepository repository, 
            IMapper mapper, 
            INotificationService notification)
            : base (mapper, notification)
        {
            _payMethodFromLaunchRepository = repository;
        }

        public async Task<PayMethodFromLaunch> CreateAsync(PayMethodFromLaunch payMethodFromLaunch, int launchId)
        {
            payMethodFromLaunch.LaunchId = launchId;
            payMethodFromLaunch.CreatedAt = DateTime.Now;
            payMethodFromLaunch.UpdatedAt = payMethodFromLaunch.DeletedAt = null;

            var result = await _payMethodFromLaunchRepository.AddAsync(payMethodFromLaunch);
            if (result != Success)
            {
                // msg: there was a problem to create launch
                Notification.InfraRaiseError(PayMethodFromLaunch.Error.ErrorToCreatePayMethodFromLaunch);
            }

            return payMethodFromLaunch;
        }

        public async Task SoftDeleteAsync(PayMethodFromLaunch payMethodFromLaunch)
        {
            payMethodFromLaunch.DeletedAt = DateTime.Now;
            var result = await _payMethodFromLaunchRepository.UpdateAsync(payMethodFromLaunch);
            if (result != Success)
            {
                // msg: there was a problem to delete launch
                Notification.InfraRaiseError(PayMethodFromLaunch.Error.ErrorToDeletePayMethodFromLaunch);
            }
        }

        public async Task<PayMethodFromLaunchResponseDto> UpdateAsync(PayMethodFromLaunchRequestDto payMethodRequest, int launchId)
        {
            var payMethodFromLaunch = _mapper.Map<PayMethodFromLaunch>(payMethodRequest);
            payMethodFromLaunch.LaunchId = launchId;
            payMethodFromLaunch.UpdatedAt = DateTime.Now;

            var result = await _payMethodFromLaunchRepository.UpdateAsync(payMethodFromLaunch);
            if (result != Success)
            {
                // msg: there was a problem to update launch
                Notification.InfraRaiseError(PayMethodFromLaunch.Error.ErrorToUpdatePayMethodFromLaunch);
            }

            var payMethodFromLaunchResponse = _mapper.Map<PayMethodFromLaunchResponseDto>(payMethodFromLaunch);
            return payMethodFromLaunchResponse;
        }

        public async Task<PayMethodFromLaunchResponseDto> GetAsync(int launchId)
        {
            var result = await _payMethodFromLaunchRepository.FindRelationAsync(launchId);

            var payMethodFromLaunchResponse = _mapper.Map<PayMethodFromLaunchResponseDto>(result);
            return payMethodFromLaunchResponse;
        }

        public async Task<PayMethodFromLaunch> GetByLaunchIdWithoutDtoAsync(int launchId)
        {
            var result = await _payMethodFromLaunchRepository.FindRelationAsync(launchId);
            return result;
        }

        public async Task<List<PayMethodFromLaunch>> ListAsync(List<int> launchIds)
        {
            var result = await _payMethodFromLaunchRepository.ListRelationsAsync(launchIds);
            return result;
        }
    }
}
