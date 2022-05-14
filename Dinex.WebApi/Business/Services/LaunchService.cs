namespace Dinex.WebApi.Business
{
    public class LaunchService : ILaunchService
    {
        private readonly ILaunchRepository _launchRepository;
        private readonly IPayMethodFromLaunchService _payMethodFromLaunchService;

        public LaunchService(
            ILaunchRepository launchRepository, 
            IPayMethodFromLaunchService payMethodFromLaunchService)
        {
            _launchRepository = launchRepository;
            _payMethodFromLaunchService = payMethodFromLaunchService;
        }

        public async Task<(Launch, PayMethodFromLaunch?)> CreateAsync(Launch launch, PayMethodFromLaunch? payMethodFromLaunch)
        {
            launch.CreatedAt = DateTime.Now;
            launch.UpdatedAt = launch.DeletedAt = null;
            var launchResult = await _launchRepository.AddAsync(launch);

            if(launchResult != 1)
                return (null, null);

            if(payMethodFromLaunch is not null)
            {
                payMethodFromLaunch.LaunchId = launch.Id;
                var payMethodResult= await _payMethodFromLaunchService.CreateAsync(payMethodFromLaunch);

                return (launch, payMethodResult);
            }

            return (launch, null);
        }

        public async Task<(Launch, PayMethodFromLaunch?)> UpdateAsync(Launch launch, PayMethodFromLaunch? payMethodFromLaunch)
        {
            var launchResult = await _launchRepository.UpdateAsync(launch);

            if (launchResult != 1)
                return (null, null);

            if (payMethodFromLaunch is not null)
            {
                payMethodFromLaunch.LaunchId = launch.Id;
                var payMethodResult = await _payMethodFromLaunchService.UpdateAsync(payMethodFromLaunch);

                return (launch, payMethodResult);
            }

            return (launch, payMethodFromLaunch);
        }

        public async Task<bool> DeleteAsync(int launchId)
        {
            var (launch, payMethodFromLaunch) = await GetAsync(launchId);

            if(launch is null)
                return false;

            await _launchRepository.DeleteAsync(launch);
            
            if (payMethodFromLaunch != null)
            {
                var payMethodResult = await _payMethodFromLaunchService.SoftDeleteAsync(payMethodFromLaunch);
                if(!payMethodResult)
                    return false;
            }

            return true;
        }

        public async Task<(Launch, PayMethodFromLaunch?)> GetAsync(int launchId)
        {
            var launch = await _launchRepository.GetByIdAsync(launchId);

            var payMehtod = await _payMethodFromLaunchService.GetAsync(launchId);

            return (launch, payMehtod);
        }

        public Task<(List<Launch>, List<PayMethodFromLaunch>?)> ListAsync()
        {
            throw new NotImplementedException();
        }


    }
}
