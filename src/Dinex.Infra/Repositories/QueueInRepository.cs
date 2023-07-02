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

    public override async Task<int> UpdateAsync(QueueIn queueIn)
    {
        var result = await _repository.UpdateAsync(queueIn);
        return result;
    }

    public override async Task DeleteAsync(QueueIn queueIn)
    {
        await _repository.DeleteAsync(queueIn);
    }

    public async Task<IEnumerable<QueueIn>> ListQueueInAsync()
    {
        var result = await _context.QueueIn
                .Where(x => x.UpdatedAt == null)
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();
        return result;
    }
}
