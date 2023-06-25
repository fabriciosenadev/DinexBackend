namespace Dinex.Business;

public interface IQueueInService
{
    Task<QueueIn> CreateAsync(QueueIn queueIn);
}
