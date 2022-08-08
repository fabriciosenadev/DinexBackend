namespace Dinex.Business
{ 
    public interface IUserAmountAvailableService
    {
        Task<UserAmountAvailableResponseDto> GetAmountAvailableAsync(Guid userId);
        Task<UserAmountAvailable> CreateAsync(UserAmountAvailable userAmountAvailable);
        Task UpdateAsync(UserAmountAvailable userAmountAvailable);
    }
}
