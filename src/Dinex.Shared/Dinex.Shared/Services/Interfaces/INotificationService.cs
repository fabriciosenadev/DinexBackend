namespace Dinex.Shared;

public interface INotificationService
{
    //void RaiseError<T>(T enumError, Notification.Type errorType = Notification.Type.App) where T : Enum;
    void RaiseError<T>(T enumError) where T : Enum;
    void RaiseError(NotificationDto notification);
    bool HasNotification();
    List<NotificationDto> GetAllNotifications();
}
