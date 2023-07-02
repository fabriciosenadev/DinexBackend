namespace Dinex.Infra
{
    public interface IHistoryFileRepository
    {
        Task AddRangeAsync(List<InvestingHistoryFile> historyFiles);
        Task DeleteAsync(InvestingHistoryFile investingHistoryFile);
        Task<IEnumerable<InvestingHistoryFile>> ListHistoryFilesAsync(Guid queueInId);
    }
}
