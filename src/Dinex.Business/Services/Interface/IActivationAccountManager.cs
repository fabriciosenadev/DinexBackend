namespace Dinex.Business
{
    public interface IActivationAccountManager
    {
        Task ActivateAccountAsync(string email, string activationCode);
        Task<string> SendActivationCodeAsync(string email);
    }
}
