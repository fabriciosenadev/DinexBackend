namespace Dinex.Business;

public interface IProcessingService
{
    Task ProcessQueueIn(Guid userId);
}
