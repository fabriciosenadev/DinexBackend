namespace Dinex.WebApi.Infra
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<Category>> ListStandardCategories();
    }
}
