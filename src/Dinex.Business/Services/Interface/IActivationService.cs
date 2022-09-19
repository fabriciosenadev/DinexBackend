namespace Dinex.Business
{
    public interface IActivationService
    {
        Task ValidateActivationCode(string activationCode, Guid userId);
        Task ClearActivationCodesAsync(Guid userId);
        string GenerateActivatioCodeAsync(int codeLength);
        Task<int> AddActivationOnDatabaseAsync(Guid userId, string activationCode);
    }
}
