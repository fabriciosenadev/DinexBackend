namespace Dinex.Shared;

public interface IEmailService
{
    Task<string> SendByTemplateAsync(SendEmailDto sendEmailDto);
}