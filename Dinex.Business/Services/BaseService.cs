namespace Dinex.Business
{
    public class BaseService
    {
        public const int Success = 1;
        public const int InvalidId = 0;

        public readonly INotificationService Notification;
        public readonly IMapper _mapper;
        public BaseService(IMapper mapper, INotificationService notification)
        {
            _mapper = mapper;
            Notification = notification;
        }
    }
}
