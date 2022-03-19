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
    }
}
