namespace Dinex.Infra
{
    public class HistoryFileRepository : Repository<InvestingHistoryFile>, IHistoryFileRepository
    {
        private readonly IRepository<InvestingHistoryFile> _repository;
        public HistoryFileRepository(DinexBackendContext context, IRepository<InvestingHistoryFile> repository) : base(context)
        {
            _repository = repository;
        }

        public async Task AddRangeAsync(List<InvestingHistoryFile> historyFiles)
        {
            await _context.InvestingHistoryFiles.AddRangeAsync(historyFiles);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(InvestingHistoryFile investingHistoryFile)
        {
            await _repository.DeleteAsync(investingHistoryFile);
        }

        public async Task<IEnumerable<InvestingHistoryFile>> ListHistoryFilesAsync(Guid queueInId)
        {
            var result = await _context.InvestingHistoryFiles
                .Where(x => x.QueueId == queueInId)
                    .OrderBy(x => x.CreatedAt)
                    .ToListAsync();
            return result;
        }
    }
}
