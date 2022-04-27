namespace Dinex.WebApi.Infra
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<Category>> ListStandardCategoriesAsync();

        Task<Category> GetByNameAsync(string categoryName);

        Task<Category> GetByIdAsync(int categoryId);

        Task<List<Category>> ListCategoriesAsync(List<int> listCategoryRelationIds);
    }
}
