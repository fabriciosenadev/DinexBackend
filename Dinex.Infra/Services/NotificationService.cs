namespace Dinex.Infra
{
    public class NotificationService : INotificationService
    {
        public NotificationService()
        {

        }

        public void AppRaiseError<T>(T enumError) where T : Enum
        {
            // TODO: need to implement a message translator
            throw new AppException(enumError.ToString());
        }

        public void InfraRaiseError<T>(T enumError) where T : Enum
        {
            // TODO: need to implement a message translator
            throw new AppException(enumError.ToString());
        }
    }
}
