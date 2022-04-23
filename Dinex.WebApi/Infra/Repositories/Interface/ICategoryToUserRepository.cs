namespace Dinex.WebApi.Infra
{
    public interface ICategoryToUserRepository : IRepository<CategoryToUser>
    {
        Task<CategoryToUser> FindRelationAsync(int categoryId, Guid userId);
        Task<CategoryToUser> FindDeletedRelationAsync(int categoryId, Guid userId);
        Task<List<int>> ListCategoryRelationIdsAsync(Guid userId);
        Task<List<int>> ListCategoryRelationIdsDeletedAsync(Guid userId);
    }
}
