namespace Dinex.Infra
{
    public class CodeManagerRepository : Repository<CodeManager>, ICodeManagerRepository
    {
        public CodeManagerRepository(DinexBackendContext context) : base(context)
        {

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
    }
}
