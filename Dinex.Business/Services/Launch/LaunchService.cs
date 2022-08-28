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
                Notification.RaiseError(
                    Launch.Error.LaunchErrorToCreate, 
                    NotificationService.ErrorType.Infra);

            return launch;
        }

        public async Task<Launch> UpdateAsync(Launch launch)
        {
            launch.UpdatedAt = DateTime.Now;

            var launchResult = await _launchRepository.UpdateAsync(launch);
            if (launchResult != Success)
                Notification.RaiseError(Launch.Error.LaunchErrorToUpdate, NotificationService.ErrorType.Infra);

            return launch;
        }

        public async Task SoftDeleteAsync(Launch launch)
        {
            launch.DeletedAt = DateTime.Now;

            var result = await _launchRepository.UpdateAsync(launch);
            if (result != Success)
                Notification.RaiseError(Launch.Error.LaunchErrorToDelete);
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

        public async Task<int> CountByCategoryIdAsync(int categoryId, Guid userId)
        {
            var count = await _launchRepository.CountByCategoryIdAsync(categoryId, userId);
            return count;
        }

        public async Task<Launch> GetByIdAsync(int launchId)
        {
            var result = await _launchRepository.GetByIdAsync(launchId);
            if (result is null)
                Notification.RaiseError(Launch.Error.LaunchNotFound);
            
            return result;
        }

        public Task<decimal> GetSumAmountByStatus(List<int> categoriesId, Guid userId, LaunchStatus launchStatus, DateTime startDate, DateTime endDate)
        {
            var result = _launchRepository.GetSumAmountByStatus(categoriesId, userId, launchStatus, startDate, endDate);
            return result;
        }
        public async Task<decimal> GetLaunchesSumByCategoriesId(Guid userId, List<int> categoriesId)
        {
            var result = await _launchRepository.GetLaunchesSumByCategoriesId(userId, categoriesId);
            return result;
        }
    }
}
