namespace Dinex.WebApi.Business
{
    public class LaunchService : ILaunchService
    {
        private readonly ILaunchRepository _launchRepository;
        private readonly IPayMethodFromLaunchService _payMethodFromLaunchService;
        private readonly ICategoryToUserService _categoryToUserService;

        public LaunchService(
            ILaunchRepository launchRepository,
            IPayMethodFromLaunchService payMethodFromLaunchService,
            ICategoryToUserService categoryToUserService)
        {
            _launchRepository = launchRepository;
            _payMethodFromLaunchService = payMethodFromLaunchService;
            _categoryToUserService = categoryToUserService;
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

        public async Task<bool> SoftDeleteAsync(int launchId)
        {
            var (launch, payMethodFromLaunch) = await GetAsync(launchId);

            if(launch is null)
                return false;

            launch.DeletedAt = DateTime.Now;
            var resultCategory = await _launchRepository.UpdateAsync(launch);

            if (resultCategory != 1)
                return false;

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

        public async Task<(List<Launch>, List<CategoryToUser>)> ListLast(Guid userId)
        {
            var launches = await _launchRepository.ListLast(userId);
            var categoriesToUser = await _categoryToUserService.ListCategoryRelationIdsAsync(userId);

            return (launches, categoriesToUser);
        }
    }
}
