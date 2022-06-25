namespace Dinex.Business
{
    public class CategoryToUserService : ICategoryToUserService
    {
        private readonly ICategoryToUserRepository _categoryToUserRepository;
        public CategoryToUserService(ICategoryToUserRepository categoryToUserRepository)
        {
            _categoryToUserRepository = categoryToUserRepository;
        }

        public async Task AssignCategoryToUserAsync(Guid userId, int categoryId, Applicable applicable)
        {
            var relation = new CategoryToUser();
            relation.UserId = userId;
            relation.CategoryId = categoryId;
            relation.Applicable = applicable;
            relation.CreatedAt = DateTime.Now;

            await _categoryToUserRepository.AddAsync(relation);
        }

        public async Task<bool> SoftDeleteRelationAsync(CategoryToUser categoryToUser)
        {
            categoryToUser.DeletedAt = DateTime.Now;
            var result = await _categoryToUserRepository.UpdateAsync(categoryToUser);
            if(result != 1)
                return false;
            return true;
        }

        public async Task<CategoryToUser> GetRelationAsync(int categoryId, Guid userId)
        {
            var result = await _categoryToUserRepository.FindRelationAsync(categoryId, userId);
            return result;
        }

        public async Task<List<CategoryToUser>> ListCategoryRelationIdsAsync(Guid userId)
        {
            var result = await _categoryToUserRepository.ListCategoryRelationIdsAsync(userId);
            return result;
        }

        public async Task<List<CategoryToUser>> ListCategoryRelationIdsDeletedAsync(Guid userId)
        {
            var result = await _categoryToUserRepository.ListCategoryRelationIdsDeletedAsync(userId);
            return result;
        }

        public async Task<CategoryToUser> RestoreDeletedCategoryAsync(Guid userId, int categoryId)
        {
            var relation = await _categoryToUserRepository.FindDeletedRelationAsync(categoryId, userId);

            relation.DeletedAt = null;
            var result = await _categoryToUserRepository.UpdateAsync(relation);
            return relation;
        }

        public List<int> ListCategoryIds(List<CategoryToUser> categoryToUsers)
            => categoryToUsers.Select(x => x.CategoryId).ToList();

        public string CapitalizeFirstLetter(string value)
        {
            var newStr = char.ToUpper(value[0]) + value.Substring(1);
            return newStr;
        }
    }
}
