namespace Dinex.Business
{
    public interface ICategoryService
    {
        Task<Category> CreateCategoryAsync(Category category);
        Task AddStandardCategoriesAsync();
        Task<List<Category>> ListCategoriesAsync(List<int> categoryIdsList);
        Task<List<Category>> ListStandardCategoriesAsync();
        Task<Category> GetByNameAsync(string categoryName);
        Task<Category> GetByIdAsync(int categoryId);
        void ValidateCategoryId(int categoryId);
    }
}
