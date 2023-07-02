namespace Dinex.Infra;

public interface IQueueInRepository
{
    Task<int> AddQueueInAsync(QueueIn queueIn);
    Task<int> UpdateAsync(QueueIn queueIn);
    Task DeleteAsync(QueueIn queueIn);
    Task<IEnumerable<QueueIn>> ListQueueInAsync();
}
