namespace Dinex.Business
{
    public class PayMethodFromLaunchService : IPayMethodFromLaunchService
    {
        private readonly IPayMethodFromLaunchRepository _repository;
        private readonly IMapper _mapper;

        public PayMethodFromLaunchService(IPayMethodFromLaunchRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PayMethodFromLaunch> CreateAsync(PayMethodFromLaunch payMethodFromLaunch, int launchId)
        {
            payMethodFromLaunch.LaunchId = launchId;
            payMethodFromLaunch.CreatedAt = DateTime.Now;
            payMethodFromLaunch.UpdatedAt = payMethodFromLaunch.DeletedAt = null;

            var result = await _repository.AddAsync(payMethodFromLaunch);
            if (result != 1)
            {
                // msg: there was a problem to create launch
                throw new InfraException(PayMethodFromLaunch.Error.ErrorToCreatePayMethodFromLaunch.ToString());
            }

            return payMethodFromLaunch;
        }

        public async Task SoftDeleteAsync(PayMethodFromLaunch payMethodFromLaunch)
        {
            payMethodFromLaunch.DeletedAt = DateTime.Now;
            var result = await _repository.UpdateAsync(payMethodFromLaunch);
            if (result != 1)
            {
                // msg: there was a problem to delete launch
                throw new InfraException(PayMethodFromLaunch.Error.ErrorToDeletePayMethodFromLaunch.ToString());
            }
        }

        public async Task<PayMethodFromLaunchResponseDto> UpdateAsync(PayMethodFromLaunchRequestDto payMethodRequest, int launchId)
        {
            var payMethodFromLaunch = _mapper.Map<PayMethodFromLaunch>(payMethodRequest);
            payMethodFromLaunch.LaunchId = launchId;
            payMethodFromLaunch.UpdatedAt = DateTime.Now;

            var result = await _repository.UpdateAsync(payMethodFromLaunch);
            if (result != 1)
            {
                // msg: there was a problem to update launch
                throw new InfraException(PayMethodFromLaunch.Error.ErrorToUpdatePayMethodFromLaunch.ToString());
            }

            var payMethodFromLaunchResponse = _mapper.Map<PayMethodFromLaunchResponseDto>(payMethodFromLaunch);
            return payMethodFromLaunchResponse;
        }

        public async Task<PayMethodFromLaunchResponseDto> GetAsync(int launchId)
        {
            var result = await _repository.FindRelationAsync(launchId);

            var payMethodFromLaunchResponse = _mapper.Map<PayMethodFromLaunchResponseDto>(result);
            return payMethodFromLaunchResponse;
        }

        public async Task<PayMethodFromLaunch> GetByLaunchIdWithoutDtoAsync(int launchId)
        {
            var result = await _repository.FindRelationAsync(launchId);
            return result;
        }

        public async Task<List<PayMethodFromLaunch>> ListAsync(List<int> launchIds)
        {
            var result = await _repository.ListRelationsAsync(launchIds);
            return result;
        }
    }
}
