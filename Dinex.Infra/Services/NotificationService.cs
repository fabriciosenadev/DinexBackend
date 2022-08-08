namespace Dinex.Infra
{
    public class NotificationService : INotificationService
    {
        public NotificationService()
        {

        }

        private void AppRaiseError<T>(T enumError) where T : Enum
        {
            // TODO: need to implement a message translator
            throw new AppException(enumError.ToString());
        }

        private void InfraRaiseError<T>(T enumError) where T : Enum
        {
            // TODO: need to implement a message translator
            throw new InfraException(enumError.ToString());
        }

        public void RaiseError<T>(T enumError, string errorType = "app") where T : Enum
        {
            switch (errorType)
            {
                case "app":
                    AppRaiseError(enumError);
                    break;
                case "infra":
                    InfraRaiseError(enumError);
                    break;
            }

        }
    }
}
