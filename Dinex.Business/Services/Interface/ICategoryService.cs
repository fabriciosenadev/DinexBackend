namespace Dinex.Business
{
    public interface ICategoryService
    {
        Task BindStandardCategoriesAsync(Guid userId);
        Task<Category> CreateAsync(Category category, Guid userId, string applicable);
        Task<bool> DeleteAsync(int categoryId, Guid userId);
        Task<Category> GetCategoryAsync(int categoryId, Guid userId);
        Task<(List<Category>, List<CategoryToUser>)> ListCategoriesAsync(Guid userId);
        Task<(List<Category>, List<CategoryToUser>)> ListCategoriesDeletedAsync(Guid userId);
        Task<Category> RestoreDeletedCategoryAsync(Guid userId, int categoryId);
    }
}
