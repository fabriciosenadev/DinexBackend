namespace Dinex.WebApi.Business
{
    public class PayMethodFromLaunchService : IPayMethodFromLaunchService
    {
        private readonly IPayMethodFromLaunchRepository _repository;

        public PayMethodFromLaunchService(IPayMethodFromLaunchRepository repository)
        {
            _repository = repository;
        }

        public async Task<PayMethodFromLaunch> CreateAsync(PayMethodFromLaunch payMethodFromLaunch)
        {
            payMethodFromLaunch.CreatedAt = DateTime.Now;
            payMethodFromLaunch.UpdatedAt = payMethodFromLaunch.DeletedAt = null;
            var result = await _repository.AddAsync(payMethodFromLaunch);
            if (result != 1)
                return null;

            return payMethodFromLaunch;
        }

        public async Task<bool> SoftDeleteAsync(PayMethodFromLaunch payMethodFromLaunch)
        {
            payMethodFromLaunch.DeletedAt = DateTime.Now;
            var result = await _repository.UpdateAsync(payMethodFromLaunch);
            if (result != 1)
                return false;
            return true;
        }

        public async Task<PayMethodFromLaunch> UpdateAsync(PayMethodFromLaunch payMethodFromLaunch)
        {
            payMethodFromLaunch.UpdatedAt = DateTime.Now;
            var result = await _repository.UpdateAsync(payMethodFromLaunch);
            if (result != 1)
                return null;

            return payMethodFromLaunch;
        }

        public Task<PayMethodFromLaunch> GetAsync(int launchId)
        {
            var result = _repository.FindRelationAsync(launchId);
            return result;
        }

        public Task<List<PayMethodFromLaunch>> ListAsync(List<int> launchIds)
        {
            var result = _repository.ListRelationsAsync(launchIds);
            return result;
        }
    }
}
