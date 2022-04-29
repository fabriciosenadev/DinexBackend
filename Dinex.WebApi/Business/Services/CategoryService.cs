namespace Dinex.WebApi.Business
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryToUserService _categoryToUserService;

        public CategoryService(
            ICategoryRepository categoryRepository,
            ICategoryToUserService categoryToUserService
            )
        {
            _categoryRepository = categoryRepository;
            _categoryToUserService = categoryToUserService;
        }

        private async Task AddStandardCategoriesAsync()
        {
            await AddCategoryAsync("Salário", Applicable.In, false);
            await AddCategoryAsync("Alimentação", Applicable.Out, false);
            await AddCategoryAsync("Beleza", Applicable.Out, false);
            await AddCategoryAsync("Educação", Applicable.Out, false);
            await AddCategoryAsync("Lazer", Applicable.Out, false);
            await AddCategoryAsync("Saúde", Applicable.Out, false);
            await AddCategoryAsync("Transporte", Applicable.Out, false);
        }

        private async Task<Category> AddCategoryAsync(string categoryName, Applicable applicable, Boolean isCustom)
        {
            var category = new Category
            {
                Name = categoryName,
                IsCustom = isCustom,
                Applicable = applicable,
                CreatedAt = DateTime.Now
            };

            await _categoryRepository.AddAsync(category);
            return category;
        }

        private async Task AssignStandardCategoriesToUserAsync(Guid userId, List<Category> standardCategories)
        {
            foreach (var category in standardCategories)
                await _categoryToUserService.AssignCategoryToUserAsync(userId, category.Id);

        }

        private async Task AssignCategoryToUserAsync(Guid userId, int categoryId) 
            => await _categoryToUserService.AssignCategoryToUserAsync(userId, categoryId);

        private async Task<Category> GetByNameAsync(string categoryName)
        {
            var category = await _categoryRepository.GetByNameAsync(categoryName);
            return category;
        }

        private async Task<CategoryToUser> GetCategoryToUserRelation(int categoryId, Guid userId)
        {
            var result = await _categoryToUserService.GetRelationAsync(categoryId, userId);
            return result;
        }

        public async Task BindStandardCategoriesAsync(Guid userId)
        {
            var standardCategories = await _categoryRepository.ListStandardCategoriesAsync();
            if (standardCategories.Count == 0)
            {
                await AddStandardCategoriesAsync();
                standardCategories = await _categoryRepository.ListStandardCategoriesAsync();
            }

            await AssignStandardCategoriesToUserAsync(userId, standardCategories);
        }

        public async Task<Category> CreateAsync(Category category, Guid userId)
        {
            var foundCategory = await GetByNameAsync(category.Name);

            Category result = foundCategory;
            if (foundCategory is null)
                result = await AddCategoryAsync(category.Name, category.Applicable, true);

            var hasRelation = await GetCategoryToUserRelation((int)result.Id, userId);
            if (hasRelation is not null)
                return null;

            await AssignCategoryToUserAsync(userId, result.Id);

            return result;
        }

        public async Task<bool> DeleteAsync(int categoryId, Guid userId)
        {
            if (categoryId == 0)
                return false;

            var relation = await _categoryToUserService.GetRelationAsync(categoryId, userId);
            if (relation is null)
                return false;

            var result = await _categoryToUserService.SoftDeleteRelationAsync(relation);
            return result;
        }

        public async Task<Category> GetCategoryAsync(int categoryId, Guid userId)
        {
            if (categoryId == 0)
                return null;

            var relation = await _categoryToUserService.GetRelationAsync(categoryId, userId);
            if (relation is null)
                return null;

            var category = await _categoryRepository.GetByIdAsync(categoryId);
            return category;
        }

        public async Task<List<Category>> ListCategoriesAsync(Guid userId)
        {
            var listCategoryRelationIds = await _categoryToUserService.ListCategoryRelationIdsAsync(userId);

            if (listCategoryRelationIds is null)
                return new List<Category>();

            var listCategories = await _categoryRepository.ListCategoriesAsync(listCategoryRelationIds);

            if (listCategories is null)
                return new List<Category>();

            return listCategories;
        }

        public async Task<List<Category>> ListCategoriesDeletedAsync(Guid userId)
        {
            var listCategoryRelationIds = await _categoryToUserService.ListCategoryRelationIdsDeletedAsync(userId);

            if (listCategoryRelationIds is null)
                return new List<Category>();

            var listCategories = await _categoryRepository.ListCategoriesAsync(listCategoryRelationIds);

            if (listCategories is null)
                return new List<Category>();

            return listCategories;
        }

        public async Task<Category> RestoreDeletedCategoryAsync(Guid userId, int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category is null)
                return null;

            var result = await _categoryToUserService.RestoreDeletedCategoryAsync(userId, categoryId);
            if (result is null)
                return null;

            return category;
        }
    }
}
