namespace Dinex.WebApi.Infra
{
    public interface ISendMailService
    {
        Task<string> SendActivationCodeAsync(string activationCode, string fullName, string to);
    }
}
