namespace Dinex.WebApi.Infra
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsNoTracking(Guid id);
    }
}
