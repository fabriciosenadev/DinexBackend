using Newtonsoft.Json;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Dinex.Infra
{
    public class NotificationService : INotificationService
    {
        public enum ErrorType
        {
            App,
            Infra
        }

        public NotificationService()
        {

        }

        private void AppRaiseError<TEnum>(TEnum enumError) where TEnum : Enum
        {
            var errorMessage = GetErrorMessage(enumError.ToString()) ?? enumError.ToString();            
            throw new AppException(errorMessage);
        }

        private void InfraRaiseError<T>(T enumError) where T : Enum
        {
            var errorMessage = GetErrorMessage(enumError.ToString()) ?? enumError.ToString();
            throw new InfraException(errorMessage);
        }

        private string GetErrorMessage(string enumError)
        {
            var fileName = "messages.pt-br.json";
            var filePath = "Localization/" + fileName;

            Dictionary<string, string> messages;
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                messages = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }

            var message = messages.FirstOrDefault(x => x.Key.Contains(enumError)).Value;
            return message;
        }

        public void RaiseError<T>(T enumError, ErrorType errorType = ErrorType.App) where T : Enum
        {
            switch (errorType)
            {
                case ErrorType.App:
                    AppRaiseError(enumError);
                    break;
                case ErrorType.Infra:
                    InfraRaiseError(enumError);
                    break;
            }

        }
    }
}
