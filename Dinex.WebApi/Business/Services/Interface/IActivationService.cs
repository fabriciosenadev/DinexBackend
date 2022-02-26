namespace Dinex.WebApi.Business
{
    public interface IActivationService
    {
        Task<string> SendActivationCode(string email);
        Task ActivateAccount(string email, string activationCode);
    }
}
