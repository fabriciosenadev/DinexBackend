namespace Dinex.WebApi.Infra
{
    public interface ILaunchRepository : IRepository<Launch>
    {
        Task<Launch> GetByIdAsync(int launchId);
    }
}
