namespace Dinex.Infra
{
    public interface IUserRepository
    {
        Task<int> AddUserAsync(User user);
        Task<int> UpdateUserAsync(User user);
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsNoTracking(Guid id);
    }
}
