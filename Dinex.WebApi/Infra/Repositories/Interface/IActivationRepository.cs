namespace Dinex.WebApi.Infra
{
    public interface IActivationRepository : IRepository<Activation>
    {
        Task<List<Activation>> ListByUserIdAsync(Guid userId);
        
    }
}
