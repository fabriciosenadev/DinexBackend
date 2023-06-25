namespace Dinex.Infra
{
    public interface IHistoryFileRepository
    {
        Task AddRangeAsync(List<HistoryFile> historyFiles);
    }
}
