namespace Dinex.WebApi.Infra
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<Category>> ListStandardCategories();

        Task<Category> GetByName(string categoryName);

        Task<Category> GetByIdAsync(int categoryId);

        Task<List<Category>> ListCategoriesAsync(List<int> listCategoryRelationIds);
    }
}
