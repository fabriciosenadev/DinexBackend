namespace Dinex.WebApi.Business
{
    public interface ICategoryService
    {
        Task BindStandardCategoriesAsync(Guid userId);
        Task<Category> CreateAsync(Category category, Guid userId);
        Task<bool> DeleteAsync(int categoryId, Guid userId);
        Task<Category> GetCategoryAsync(int categoryId, Guid userId);
        Task<List<Category>> ListCategoriesAsync(Guid userId);
        Task<List<Category>> ListCategoriesDeletedAsync(Guid userId);
        Task<Category> RestoreDeletedCategoryAsync(Guid userId, int categoryId);
    }
}
