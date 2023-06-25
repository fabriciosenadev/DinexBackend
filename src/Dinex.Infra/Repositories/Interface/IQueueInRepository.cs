namespace Dinex.Infra;

public interface IQueueInRepository
{
    Task<int> AddQueueInAsync(QueueIn queueIn);
}
