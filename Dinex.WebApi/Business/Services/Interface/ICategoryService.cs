namespace Dinex.WebApi.Business
{
    public interface ICategoryService
    {
        Task BindStandardCategories(Guid userId);
        Task<Category> Create(Category category, Guid userId);
        Task<bool> Delete(int categoryId, Guid userId);
        Task<Category> GetCategory(int categoryId, Guid userId);
        Task<List<Category>> ListCategories(Guid userId);
        Task<List<Category>> ListCategoriesDeleted(Guid userId);
        Task<Category> RestoreDeletedCategory(Guid userId, int categoryId);
    }
}
