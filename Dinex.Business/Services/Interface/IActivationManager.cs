namespace Dinex.Business
{
    public interface IActivationManager
    {
        Task ActivateAccountAsync(string email, string activationCode);
        Task<string> SendActivationCodeAsync(string email);
    }
}
