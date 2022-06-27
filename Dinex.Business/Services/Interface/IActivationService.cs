namespace Dinex.Business
{
    public interface IActivationService
    {
        Task<string> SendActivationCodeAsync(string email);
        Task ActivateAccountAsync(string email, string activationCode);
    }
}
