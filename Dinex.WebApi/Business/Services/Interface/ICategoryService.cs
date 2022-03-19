namespace Dinex.WebApi.Business
{
    public interface ICategoryService
    {
        Task BindStandardCategories(Guid userId);
    }
}
