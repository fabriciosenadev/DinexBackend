namespace Dinex.Business
{ 
    public interface IUserAmountAvailableService
    {
        Task<UserAmountAvailable> GetAmountAvailableAsync(Guid userId);
        Task<UserAmountAvailable> CreateAsync(UserAmountAvailable userAmountAvailable);
        Task UpdateAsync(UserAmountAvailable userAmountAvailable);
    }
}
