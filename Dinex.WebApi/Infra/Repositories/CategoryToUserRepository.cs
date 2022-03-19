namespace Dinex.WebApi.Infra
{
    public class CategoryToUserRepository : Repository<CategoryToUser>, ICategoryToUserRepository
    {
        public CategoryToUserRepository(DinexBackendContext context) : base(context)
        {
        }
    }
}
