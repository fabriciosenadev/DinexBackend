namespace Dinex.WebApi.Business
{
    public interface ICategoryToUserService
    {
        Task AssignCategoryToUserAsync(Guid userId, int categoryId, Applicable applicable);

        Task<CategoryToUser> GetRelationAsync(int categoryId, Guid userId);

        Task<bool> SoftDeleteRelationAsync(CategoryToUser categoryToUser);

        Task<List<CategoryToUser>> ListCategoryRelationIdsAsync(Guid userId);

        Task<List<CategoryToUser>> ListCategoryRelationIdsDeletedAsync(Guid userId);

        Task<CategoryToUser> RestoreDeletedCategoryAsync(Guid userId, int categoryId);

        List<int> ListCategoryIds(List<CategoryToUser> categoryToUsers);

        string CapitalizeFirstLetter(string value);
    }
}
