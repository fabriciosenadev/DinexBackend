namespace Dinex.Infra
{
    public interface IActivationRepository : IRepository<Activation>
    {
        Task<List<Activation>> ListByUserIdAsync(Guid userId);
        Task DeleteByUserIdAsync(Guid userId);
        
    }
}
