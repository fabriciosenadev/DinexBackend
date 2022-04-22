namespace Dinex.WebApi.Infra
{
    public interface ICategoryToUserRepository : IRepository<CategoryToUser>
    {
        Task<CategoryToUser> FindRelationAsync(int categoryId, Guid userId);
        Task<List<int>> ListCategoryRelationIdsAsync(Guid userId);
    }
}
