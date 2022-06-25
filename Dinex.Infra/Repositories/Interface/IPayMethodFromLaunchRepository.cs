namespace Dinex.Infra
{
    public interface IPayMethodFromLaunchRepository : IRepository<PayMethodFromLaunch>
    {
        Task<PayMethodFromLaunch> FindRelationAsync(int launchId);
        Task<List<PayMethodFromLaunch>> ListRelationsAsync(List<int> launchIds);
    }
}
