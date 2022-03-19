namespace Dinex.WebApi.Business
{
    public interface ICategoryToUserService
    {
        Task AssignCategoryToUser(Guid userId, int categoryId);
    }
}
