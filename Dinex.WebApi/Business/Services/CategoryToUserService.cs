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

        public async Task<bool> SoftDeleteRelation(CategoryToUser categoryToUser)
        {
            categoryToUser.DeletedAt = DateTime.Now;
            var result = await _categoryToUserRepository.UpdateAsync(categoryToUser);
            if(result != 1)
                return false;
            return true;
        }

        public async Task<CategoryToUser> GetRelation(int categoryId, Guid userId)
        {
            var result = await _categoryToUserRepository.FindRelationAsync(categoryId, userId);
            return result;
        }

        public async Task<List<int>> ListCategoryRelationIds(Guid userId)
        {
            var result = await _categoryToUserRepository.ListCategoryRelationIdsAsync(userId);
            return result;
        }

        public async Task<List<int>> ListCategoryRelationIdsDeleted(Guid userId)
        {
            var result = await _categoryToUserRepository.ListCategoryRelationIdsDeletedAsync(userId);
            return result;
        }

        public async Task<CategoryToUser> RestoreDeletedCategory(Guid userId, int categoryId)
        {
            var relation = await _categoryToUserRepository.FindDeletedRelationAsync(categoryId, userId);

            relation.DeletedAt = null;
            var result = await _categoryToUserRepository.UpdateAsync(relation);
            return relation;
        }
    }
}
