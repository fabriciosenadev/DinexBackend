namespace Dinex.WebApi.Infra
{
    public interface ISendMailService
    {
        Task<string> SendActivationCode(string activationCode, string fullName, string to);
    }
}
