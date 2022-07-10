namespace Dinex.Business
{
    public class LaunchService : ILaunchService
    {
        private readonly ILaunchRepository _launchRepository;

        public LaunchService(ILaunchRepository launchRepository)
        {
            _launchRepository = launchRepository;
        }

        public async Task<Launch> CreateAsync(Launch launch, Guid userId)
        {
            launch.UserId = userId;
            launch.CreatedAt = DateTime.Now;
            launch.UpdatedAt = launch.DeletedAt = null;

            var result = await _launchRepository.AddAsync(launch);
            if (result != 1)
            {
                // msg:  there was a problem to create launch
                throw new InfraException(Launch.Error.ErrorToCreateLaunch.ToString());
            }

            return launch;
        }

        public async Task<Launch> UpdateAsync(Launch launch)
        {
            launch.UpdatedAt = DateTime.Now;

            var launchResult = await _launchRepository.UpdateAsync(launch);
            if (launchResult != 1)
            {
                // msg: there was a problem to update launch
                throw new AppException(Launch.Error.ErrorToUpdateLaunch.ToString());
            }

            return launch;
        }

        public async Task SoftDeleteAsync(Launch launch)
        {
            launch.DeletedAt = DateTime.Now;

            var result = await _launchRepository.UpdateAsync(launch);
            if (result != 1)
            {
                // msg: there was a problem to delete launch
                throw new AppException(Launch.Error.ErrorToDeleteLaunch.ToString());
            }
        }

        public Task<List<LaunchAndPayMethodResponseDto>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Launch>> ListLast(Guid userId)
        {
            var launches = await _launchRepository.ListLast(userId);
            return launches;
        }

        public async Task CheckExistsByCategoryIdAsync(int categoryId, Guid userId)
        {
            var count = await _launchRepository.CountByCategoryIdAsync(categoryId, userId);
            if(count > 0)
            {
                // msg : "Exists launch with this category"
                throw new AppException(Launch.Error.HasLaunchWithCategory.ToString());
            }
        }

        public async Task<Launch> GetByIdAsync(int launchId)
        {
            var result  = await _launchRepository.GetByIdAsync(launchId);
            if (result is null)
            {
                // msg: launch not found
                throw new AppException(Launch.Error.LaunchNotFound.ToString());
            }
            return result;
        }
    }
}
