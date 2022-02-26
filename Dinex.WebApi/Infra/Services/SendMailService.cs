﻿namespace Dinex.WebApi.Infra
{
    public class SendMailService : ISendMailService
    {
        private readonly AppSettings _appSettings;
        public SendMailService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public async Task<string> SendActivationCode(string activationCode, string fullName, string to)
        {
            var message = CreateMessage(activationCode, fullName, to);

            // --- SMTP information
            var smtpClient = new SmtpClient();
            await smtpClient.ConnectAsync(_appSettings.SmtpHost, _appSettings.SmtpPort, _appSettings.SmtpUseSsl); // --- TLS config
            await smtpClient.AuthenticateAsync(_appSettings.MailboxAddress, _appSettings.MailboxPassword);

            // --- sending message
            var result = await smtpClient.SendAsync(message);
            await smtpClient.DisconnectAsync(true);
            smtpClient.Dispose();

            return result;
        }

        private MimeMessage CreateMessage(string activationCode, string fullName, string to)
        {
            var message = new MimeMessage();

            var fromAddress = new MailboxAddress(_appSettings.MailboxName, _appSettings.MailboxAddress);
            message.From.Add(fromAddress);

            var toAddress = new MailboxAddress(fullName, to);
            message.To.Add(toAddress);

            message.Subject = "Ative sua conta | Dinheiro Exato";
            message.Body = CreateBodyToMessage(activationCode, fullName);

            return message;
        }

        private MimeEntity CreateBodyToMessage(string activationCode, string fullName)
        {
            var templateName = "activationCode.html";
            var partialTemplatePath = _appSettings.MailTemplateFolder + "/" + templateName;
            var fullTemplatePath = Path.GetFullPath(partialTemplatePath);

            var bodyBuilder = new BodyBuilder();
            var html = string.Empty;
            using (StreamReader Source = File.OpenText(fullTemplatePath))
            {
                html = Source.ReadToEnd();
            }

            bodyBuilder.HtmlBody = html
                .Replace("{name}", fullName)
                .Replace("{activationCode}", activationCode);

            var msgBody = bodyBuilder.ToMessageBody();
            return msgBody;
        }
    }
}
