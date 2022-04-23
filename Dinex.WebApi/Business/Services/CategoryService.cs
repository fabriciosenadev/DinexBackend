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

        private async Task AddStandardCategories()
        {
            await AddCategory("Salário", Applicable.In, false);
            await AddCategory("Alimentação", Applicable.Out, false);
            await AddCategory("Beleza", Applicable.Out, false);
            await AddCategory("Educação", Applicable.Out, false);
            await AddCategory("Lazer", Applicable.Out, false);
            await AddCategory("Saúde", Applicable.Out, false);
            await AddCategory("Transporte", Applicable.Out, false);
        }

        private async Task<Category> AddCategory(string categoryName, Applicable applicable, Boolean isCustom)
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

        private async Task AssignStandardCategoriesToUser(Guid userId, List<Category> standardCategories)
        {
            foreach (var category in standardCategories)
                await _categoryToUserService.AssignCategoryToUser(userId, category.Id);

        }

        private async Task AssignCategoryToUser(Guid userId, int categoryId) 
            => await _categoryToUserService.AssignCategoryToUser(userId, categoryId);

        private async Task<Category> GetByName(string categoryName)
        {
            var category = await _categoryRepository.GetByName(categoryName);
            return category;
        }

        private async Task<CategoryToUser> GetCategoryToUserRelation(int categoryId, Guid userId)
        {
            var result = await _categoryToUserService.GetRelation(categoryId, userId);
            return result;
        }

        public async Task BindStandardCategories(Guid userId)
        {
            var standardCategories = await _categoryRepository.ListStandardCategories();
            if (standardCategories.Count == 0)
            {
                await AddStandardCategories();
                standardCategories = await _categoryRepository.ListStandardCategories();
            }

            await AssignStandardCategoriesToUser(userId, standardCategories);
        }

        public async Task<Category> Create(Category category, Guid userId)
        {
            var foundCategory = await GetByName(category.Name);

            Category result = foundCategory;
            if (foundCategory is null)
                result = await AddCategory(category.Name, category.Applicable, true);

            var hasRelation = await GetCategoryToUserRelation((int)result.Id, userId);
            if (hasRelation is not null)
                return null;

            await AssignCategoryToUser(userId, result.Id);

            return result;
        }

        public async Task<bool> Delete(int categoryId, Guid userId)
        {
            if (categoryId == 0)
                return false;

            var relation = await _categoryToUserService.GetRelation(categoryId, userId);
            if (relation is null)
                return false;

            var result = await _categoryToUserService.SoftDeleteRelation(relation);
            return result;
        }

        public async Task<Category> GetCategory(int categoryId, Guid userId)
        {
            if (categoryId == 0)
                return null;

            var relation = await _categoryToUserService.GetRelation(categoryId, userId);
            if (relation is null)
                return null;

            var category = await _categoryRepository.GetByIdAsync(categoryId);
            return category;
        }

        public async Task<List<Category>> ListCategories(Guid userId)
        {
            var listCategoryRelationIds = await _categoryToUserService.ListCategoryRelationIds(userId);

            if (listCategoryRelationIds is null)
                return new List<Category>();

            var listCategories = await _categoryRepository.ListCategoriesAsync(listCategoryRelationIds);

            if (listCategories is null)
                return new List<Category>();

            return listCategories;
        }

        public async Task<List<Category>> ListCategoriesDeleted(Guid userId)
        {
            var listCategoryRelationIds = await _categoryToUserService.ListCategoryRelationIdsDeleted(userId);

            if (listCategoryRelationIds is null)
                return new List<Category>();

            var listCategories = await _categoryRepository.ListCategoriesAsync(listCategoryRelationIds);

            if (listCategories is null)
                return new List<Category>();

            return listCategories;
        }

        public async Task<Category> RestoreDeletedCategory(Guid userId, int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category is null)
                return null;

            var result = await _categoryToUserService.RestoreDeletedCategory(userId, categoryId);
            if (result is null)
                return null;

            return category;
        }
    }
}
