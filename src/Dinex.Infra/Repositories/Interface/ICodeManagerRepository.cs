namespace Dinex.Infra
{
    public interface ICodeManagerRepository : IRepository<CodeManager>
    {
        Task<List<CodeManager>> ListByUserIdAsync(Guid userId);
        Task DeleteByUserIdAsync(Guid userId, CodeReason codeReason);
    }
}
