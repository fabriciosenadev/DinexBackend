namespace Dinex.Business;

public interface IHistoryFileService
{
    Task<List<InvestingHistoryFile>> CreateAsync(IFormFile fileHistory, Guid queueInId);
}
