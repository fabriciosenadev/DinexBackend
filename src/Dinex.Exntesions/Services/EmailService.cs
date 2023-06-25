namespace Dinex.Extensions;
public class EmailService : IEmailService
{
    private readonly AppSettings _appSettings;
    public EmailService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public async Task<string> SendByTemplateAsync(SendEmailDto sendEmailDto)
    {
        var message = CreateMessage(sendEmailDto);
        var result = await SendMessageAsync(message);
        return result;
    }

    private async Task<string> SendMessageAsync(MimeMessage message)
    {
        var smtpClient = new SmtpClient();
        
        // --- TLS config
        await smtpClient.ConnectAsync(_appSettings.SmtpHost, _appSettings.SmtpPort, _appSettings.SmtpUseSsl); 

        await smtpClient.AuthenticateAsync(_appSettings.MailboxAddress, _appSettings.MailboxPassword);

        // --- sending message
        var result = await smtpClient.SendAsync(message);
        await smtpClient.DisconnectAsync(true);
        smtpClient.Dispose();

        return result;
    }

    private MimeMessage CreateMessage(SendEmailDto sendEmailDto)
    {
        var message = new MimeMessage();

        var fromAddress = new MailboxAddress(_appSettings.MailboxName, _appSettings.MailboxAddress);
        message.From.Add(fromAddress);

        var toAddress = new MailboxAddress(sendEmailDto.FullName, sendEmailDto.EmailTo);
        message.To.Add(toAddress);

        message.Subject = sendEmailDto.EmailSubject;
        message.Body = CreateBodyToMessage(sendEmailDto);

        return message;
    }

    private MimeEntity CreateBodyToMessage(SendEmailDto sendEmailDto)
    {
        var partialTemplatePath = $"{_appSettings.MailTemplateFolder}/{sendEmailDto.EmailTemplateFileName}";
        var fullTemplatePath = Path.GetFullPath(partialTemplatePath);

        var bodyBuilder = new BodyBuilder();
        var html = string.Empty;
        using (StreamReader Source = File.OpenText(fullTemplatePath))
        {
            html = Source.ReadToEnd();
        }

        var activationUrl = $"{_appSettings.AllowedHost}/{sendEmailDto.Origin}/{sendEmailDto.GeneratedCode}";

        bodyBuilder.HtmlBody = html
            .Replace(sendEmailDto.TemplateFieldToName, sendEmailDto.FullName)
            .Replace(sendEmailDto.TemplateFieldToUrl, activationUrl);

        var msgBody = bodyBuilder.ToMessageBody();
        return msgBody;
    }
}
