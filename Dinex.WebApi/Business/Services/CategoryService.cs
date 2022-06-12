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
            string[] standardCategories = new string[] { "Salário", "Alimentação", "Beleza", "Educação", "Lazer", "Saúde", "Transporte" };

            foreach (string category in standardCategories)
                await AddCategoryAsync(category, false);
        }

        private async Task<Category> AddCategoryAsync(string categoryName, Boolean isCustom)
        {
            var category = new Category
            {
                Name = categoryName,
                IsCustom = isCustom,
                CreatedAt = DateTime.Now
            };

            await _categoryRepository.AddAsync(category);
            return category;
        }

        private async Task AssignStandardCategoriesToUserAsync(Guid userId, List<Category> standardCategories)
        {
            foreach (var category in standardCategories)
            {
                if (category.Name == "Salário")
                    await _categoryToUserService.AssignCategoryToUserAsync(userId, category.Id, Applicable.In);
                else
                    await _categoryToUserService.AssignCategoryToUserAsync(userId, category.Id, Applicable.Out);

            }

        }

        private async Task AssignCategoryToUserAsync(Guid userId, int categoryId, Applicable applicable)
            => await _categoryToUserService.AssignCategoryToUserAsync(userId, categoryId, applicable);

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

        public async Task<Category> CreateAsync(Category category, Guid userId, string applicable)
        {
            var foundCategory = await GetByNameAsync(category.Name);

            Category result = foundCategory;
            if (foundCategory is null)
                result = await AddCategoryAsync(category.Name, true);

            var hasRelation = await GetCategoryToUserRelation((int)result.Id, userId);
            if (hasRelation is not null)
                return null;

            var applicableEnum = EnumConvertion
                .StringToEnum<Applicable>(_categoryToUserService.CapitalizeFirstLetter(applicable));

            await AssignCategoryToUserAsync(userId, result.Id, applicableEnum);

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

        public async Task<(List<Category>, List<CategoryToUser>)> ListCategoriesAsync(Guid userId)
        {
            var listCategoryRelations = await _categoryToUserService.ListCategoryRelationIdsAsync(userId);

            if (listCategoryRelations is null)
                return (new List<Category>(), new List<CategoryToUser>());

            var listCategories = await _categoryRepository
                .ListCategoriesAsync(_categoryToUserService.ListCategoryIds(listCategoryRelations));

            if (listCategories is null)
                return (new List<Category>(), new List<CategoryToUser>());

            return (listCategories, listCategoryRelations);
        }

        public async Task<(List<Category>, List<CategoryToUser>)> ListCategoriesDeletedAsync(Guid userId)
        {
            var listCategoryRelations = await _categoryToUserService.ListCategoryRelationIdsDeletedAsync(userId);

            if (listCategoryRelations is null)
                return (new List<Category>(), new List<CategoryToUser>());

            var listCategories = await _categoryRepository
                .ListCategoriesAsync(_categoryToUserService.ListCategoryIds(listCategoryRelations));

            if (listCategories is null)
                return (new List<Category>(), new List<CategoryToUser>());

            return (listCategories, listCategoryRelations);
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
