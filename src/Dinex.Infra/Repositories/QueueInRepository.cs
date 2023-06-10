namespace Dinex.Infra;

public class QueueInRepository : Repository<QueueIn>, IQueueInRepository
{
    private readonly IRepository<QueueIn> _repository;
    public QueueInRepository(DinexBackendContext context, 
        IRepository<QueueIn> repository) 
        : base(context)
    {
        _repository = repository;
    }

    public async Task<int> AddQueueInAsync(QueueIn queueIn)
    {
        var result = await _repository.AddAsync(queueIn);
        return result;
    }
}
