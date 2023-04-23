namespace Dinex.Business
{
    public interface IActivationService
    {
        Task ValidateActivationCode(string activationCode, Guid userId);
        Task ClearActivationCodesAsync(Guid userId);
        Task<int> AddActivationOnDatabaseAsync(Guid userId, string activationCode);
    }
}
