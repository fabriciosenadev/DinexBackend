namespace Dinex.Business
{
    public interface ICodeManagerService
    {
        Task ValidateActivationCode(string activationCode, Guid userId);
        string GenerateCode(int codeLength, CodeType generationOption = CodeType.Default);
        Task<int> AssignCodeToUserAsync(Guid userId, string code, CodeReason codeReason);
        Task ClearAllCodesByUserAsync(Guid userId, CodeReason codeReason);
    }
}
