namespace Dinex.Infra
{
    public class HistoryFileRepository : Repository<HistoryFile>, IHistoryFileRepository
    {
        private readonly IRepository<HistoryFile> _repository;
        public HistoryFileRepository(DinexBackendContext context, IRepository<HistoryFile> repository) : base(context)
        {
            _repository = repository;
        }

        public async Task AddRangeAsync(List<HistoryFile> historyFiles)
        {
            await _context.HistoryFiles.AddRangeAsync(historyFiles);
            _context.SaveChanges();
        }
    }
}
