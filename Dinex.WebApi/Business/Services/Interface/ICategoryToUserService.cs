namespace Dinex.WebApi.Business
{
    public interface ICategoryToUserService
    {
        Task AssignCategoryToUser(Guid userId, int categoryId);

        Task<CategoryToUser> GetRelation(int categoryId, Guid userId);

        Task<bool> SoftDeleteRelation(CategoryToUser categoryToUser);

        Task<List<int>> ListCategoryRelationIds(Guid userId);
    }
}
