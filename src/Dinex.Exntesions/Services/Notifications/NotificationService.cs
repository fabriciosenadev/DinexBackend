namespace Dinex.Extensions;

public partial class NotificationService : INotificationService
{
    private List<Notification> _notifications;
    public NotificationService()
    {
        _notifications = new List<Notification>();
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

    private void RaiseError<T>(T enumError, Notification.Type errorType = Notification.Type.App) where T : Enum
    {
        switch (errorType)
        {
            case Notification.Type.App:
                AppRaiseError(enumError);
                break;
            case Notification.Type.Infra:
                InfraRaiseError(enumError);
                break;
        }

    }

    public void RaiseError<T>(T enumError) where T : Enum
    {
        var message = GetErrorMessage(enumError.ToString());
        _notifications.Add(new Notification(message));
    }

    public void RaiseError(Notification notification)
    {
        _notifications.Add(notification);
    }

    public bool HasNotification()
    {
        return _notifications.Any();
    }

    public List<Notification> GetAllNotifications()
    {
        return _notifications;
    }
}
