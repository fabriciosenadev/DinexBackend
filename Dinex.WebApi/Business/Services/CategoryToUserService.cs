namespace Dinex.WebApi.Business
{
    public class CategoryToUserService : ICategoryToUserService
    {
        private readonly ICategoryToUserRepository _categoryToUserRepository;
        public CategoryToUserService(ICategoryToUserRepository categoryToUserRepository)
        {
            _categoryToUserRepository = categoryToUserRepository;
        }

        public async Task AssignCategoryToUser(Guid userId, int categoryId)
        {
            var relation = new CategoryToUser();
            relation.UserId = userId;
            relation.CategoryId = categoryId;
            relation.CreatedAt = DateTime.Now;

            await _categoryToUserRepository.AddAsync(relation);
        }
    }
}
