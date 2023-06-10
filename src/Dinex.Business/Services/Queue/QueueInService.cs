namespace Dinex.Business;

public class QueueInService : BaseService, IQueueInService
{
    private readonly IQueueInRepository _repository;
    public QueueInService(IMapper mapper, INotificationService notification
        , IQueueInRepository repository)
        : base(mapper, notification)
    {
        _repository = repository;
    }

    public async Task<QueueIn> CreateAsync(QueueIn queueIn)
    {
        var filePrefix = new Guid().ToString();
        queueIn.FileName = $"{filePrefix}_{queueIn.FileName}";

        queueIn.CreatedAt = DateTime.Now;

        await _repository.AddQueueInAsync(queueIn);
        return queueIn;
    }
}
