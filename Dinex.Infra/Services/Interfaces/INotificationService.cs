namespace Dinex.Infra
{
    public interface INotificationService
    {
        void AppRaiseError<T>(T enumError) where T : Enum;
        void InfraRaiseError<T>(T enumError) where T : Enum;
    }
}
