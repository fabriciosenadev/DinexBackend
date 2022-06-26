namespace Dinex.Business
{
    public interface IActivationService
    {
        Task<string> SendActivationCodeAsync(string email);
        Task<ActivationReason> ActivateAccountAsync(string email, string activationCode);
    }
}
