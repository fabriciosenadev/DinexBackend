namespace Dinex.WebApi.Business
{
    public interface ICategoryToUserService
    {
        Task AssignCategoryToUserAsync(Guid userId, int categoryId);

        Task<CategoryToUser> GetRelationAsync(int categoryId, Guid userId);

        Task<bool> SoftDeleteRelationAsync(CategoryToUser categoryToUser);

        Task<List<int>> ListCategoryRelationIdsAsync(Guid userId);

        Task<List<int>> ListCategoryRelationIdsDeletedAsync(Guid userId);

        Task<CategoryToUser> RestoreDeletedCategoryAsync(Guid userId, int categoryId);
    }
}
