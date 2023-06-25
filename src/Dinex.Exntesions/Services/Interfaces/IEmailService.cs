

namespace Dinex.Extensions
{
    public interface IEmailService
    {
        Task<string> SendByTemplateAsync(SendEmailDto sendEmailDto);
    }
}