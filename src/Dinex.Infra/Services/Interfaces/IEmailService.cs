namespace Dinex.Infra
{
    public interface IEmailService
    {
        Task<string> SendByTemplateAsync(SendEmailDto sendEmailDto);
    }
}