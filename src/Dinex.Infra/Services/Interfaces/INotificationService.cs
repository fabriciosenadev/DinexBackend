using static Dinex.Infra.NotificationService;

namespace Dinex.Infra
{
    public interface INotificationService
    {
        void RaiseError<T>(T enumError, ErrorType errorType = ErrorType.App) where T : Enum;
    }
}
