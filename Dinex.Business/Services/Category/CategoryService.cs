namespace Dinex.Business
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public void ValidateCategoryId(int categoryId)
        {
            if (categoryId == 0)
            {
                // msg: Category not provided
                throw new InfraException(Category.Error.CategoryNotProvided.ToString());
            }
        }

        private async Task<Category> AddCategoryAsync(string categoryName, bool isCustom)
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

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            var foundCategory = await _categoryRepository.GetByNameAsync(category.Name);

            Category result = foundCategory;
            if (foundCategory is null)
                result = await AddCategoryAsync(category.Name, true);

            return result;
        }

        public async Task AddStandardCategoriesAsync()
        {
            string[] standardCategories = new string[] { "Salário", "Alimentação", "Beleza", "Educação", "Lazer", "Saúde", "Transporte" };

            foreach (string category in standardCategories)
                await AddCategoryAsync(category, false);
        }

        public async Task<List<Category>> ListCategoriesAsync(List<int> categoryIdsList)
        {
            var categoriesList = await _categoryRepository
                .ListCategoriesAsync(categoryIdsList);

            if (categoriesList is null)
                return new List<Category>();

            return categoriesList;
        }

        public async Task<Category> GetByIdAsync(int categoryId)
        {
            var result = await _categoryRepository.GetByIdAsync(categoryId);
            return result;
        }

        public async Task<List<Category>> ListStandardCategoriesAsync()
        {
            var result = await _categoryRepository.ListStandardCategoriesAsync();
            if (result is null)
            {
                // msg: Category not found
                throw new AppException(Category.Error.CategoryNotFound.ToString());
            }
            return result;
        }

        public async Task<Category> GetByNameAsync(string categoryName)
        {
            return await _categoryRepository.GetByNameAsync(categoryName);
        }
    }
}
