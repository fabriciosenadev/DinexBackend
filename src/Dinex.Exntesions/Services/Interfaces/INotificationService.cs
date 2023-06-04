using static Dinex.Extensions.NotificationService;

namespace Dinex.Extensions
{
    public interface INotificationService
    {
        //void RaiseError<T>(T enumError, Notification.Type errorType = Notification.Type.App) where T : Enum;
        void RaiseError<T>(T enumError) where T : Enum;
        //void RaiseError(Notification notification);
        bool HasNotification();
        List<Notification> GetAllNotifications();
    }
}
