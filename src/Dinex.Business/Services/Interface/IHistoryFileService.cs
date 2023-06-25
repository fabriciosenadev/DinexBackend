namespace Dinex.Business;

public interface IHistoryFileService
{
    Task<List<HistoryFile>> CreateAsync(IFormFile fileHistory, Guid queueInId);
}
