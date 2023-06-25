namespace Dinex.Infra
{
    public class CodeManagerRepository : Repository<CodeManager>, ICodeManagerRepository
    {
        private readonly IRepository<CodeManager> _repository;
        public CodeManagerRepository(DinexBackendContext context, IRepository<CodeManager> repository) : base(context)
        {
            _repository = repository;
        }

        private async Task<IQueryable<CodeManager>> GetByUserIdAndReason(Guid userId, CodeReason codeReason)
        {
            var entities = _context.CodeManager.Where(
                a =>
                        a.UserId.Equals(userId) &&
                        a.Code.Equals(codeReason)
                    );
            return entities;
        }

        public async Task DeleteByUserIdAsync(Guid userId, CodeReason codeReason)
        {
            var entities = await GetByUserIdAndReason(userId, codeReason);
            _context.CodeManager.RemoveRange(entities);
            await SaveChangesAsync();
        }

        public async Task<List<CodeManager>> ListByUserIdAsync(Guid userId)
        {
            return await _context.CodeManager.Where(a => a.UserId.Equals(userId)).ToListAsync();
        }

        public async Task<int> CreateAsync(CodeManager codeManager)
        {
            return await _repository.AddAsync(codeManager);
        }
    }
}
