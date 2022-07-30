namespace Dinex.Business
{
    public class LaunchService : BaseService, ILaunchService
    {
        private readonly ILaunchRepository _launchRepository;

        public LaunchService(ILaunchRepository launchRepository, 
            IMapper mapper, 
            INotificationService notification) 
            : base(mapper, notification)
        {
            _launchRepository = launchRepository;
        }

        public async Task<Launch> CreateAsync(Launch launch, Guid userId)
        {
            launch.UserId = userId;
            launch.CreatedAt = DateTime.Now;
            launch.UpdatedAt = launch.DeletedAt = null;

            var result = await _launchRepository.AddAsync(launch);
            if (result != Success)
            {
                // msg:  there was a problem to create launch
                Notification.InfraRaiseError(Launch.Error.ErrorToCreateLaunch);
            }

            return launch;
        }

        public async Task<Launch> UpdateAsync(Launch launch)
        {
            launch.UpdatedAt = DateTime.Now;

            var launchResult = await _launchRepository.UpdateAsync(launch);
            if (launchResult != Success)
            {
                // msg: there was a problem to update launch
                Notification.InfraRaiseError(Launch.Error.ErrorToUpdateLaunch);
            }

            return launch;
        }

        public async Task SoftDeleteAsync(Launch launch)
        {
            launch.DeletedAt = DateTime.Now;

            var result = await _launchRepository.UpdateAsync(launch);
            if (result != Success)
            {
                // msg: there was a problem to delete launch
                Notification.AppRaiseError(Launch.Error.ErrorToDeleteLaunch);
            }
        }

        public Task<List<Launch>> ListAsync(DateTime startDate, DateTime endDate, Guid userId)
        {
            var result = _launchRepository.ListAsync(startDate, endDate, userId);
            return result;
        }

        public async Task<List<Launch>> ListLast(Guid userId)
        {
            var launches = await _launchRepository.ListLast(userId);
            return launches;
        }

        public async Task CheckExistsByCategoryIdAsync(int categoryId, Guid userId)
        {
            var count = await _launchRepository.CountByCategoryIdAsync(categoryId, userId);
            if (count > 0)
            {
                // msg : "Exists launch with this category"
                Notification.AppRaiseError(Launch.Error.HasLaunchWithCategory);
            }
        }

        public async Task<Launch> GetByIdAsync(int launchId)
        {
            var result = await _launchRepository.GetByIdAsync(launchId);
            if (result is null)
            {
                // msg: launch not found
                Notification.AppRaiseError(Launch.Error.LaunchNotFound);
            }
            return result;
        }

        public Task<decimal> GetSumAmountByStatus(List<int> categoryIds, Guid userId, LaunchStatus launchStatus, DateTime startDate, DateTime endDate)
        {
            var result = _launchRepository.GetSumAmountByStatus(categoryIds, userId, launchStatus, startDate, endDate);
            return result;
        }
    }
}
