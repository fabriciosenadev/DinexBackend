namespace Dinex.Infra
{
    public interface ISendMailService
    {
        [Obsolete("This method no longer will be used")]
        Task<string> SendActivationCodeAsync(string activationCode, string fullName, string to);
        Task<string> SendActivationCodeAsync(SendEmailDto sendEmailDto);
    }
}
