﻿namespace Dinex.Business
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(
            ICategoryRepository categoryRepository, 
            IMapper mapper,
            INotificationService notification)
            : base(mapper, notification)
        {
            _categoryRepository = categoryRepository;
        }
        //public void ValidateCategoryId(int categoryId)
        //{
        //    if (categoryId == InvalidId)
        //        Notification.RaiseError(Category.Error.CategoryNotFound);
        //    //Notification.RaiseError(
        //    //        Category.Error.CategoryNotFound, 
        //    //        NotificationService.ErrorType.Infra);
        //}

        //private async Task<Category> AddCategoryAsync(string categoryName, bool isCustom)
        //{
        //    var category = new Category
        //    {
        //        Name = categoryName,
        //        IsCustom = isCustom,
        //        CreatedAt = DateTime.Now
        //    };

        //    await _categoryRepository.AddAsync(category);
        //    return category;
        //}

        //public async Task<Category> CreateCategoryAsync(Category category)
        //{
        //    var foundCategory = await _categoryRepository.GetByNameAsync(category.Name);

        //    Category result = foundCategory;
        //    if (foundCategory is null)
        //        result = await AddCategoryAsync(category.Name, true);

        //    return result;
        //}

        //public async Task AddStandardCategoriesAsync()
        //{
        //    string[] standardCategories = new string[] { 
        //        "Salário", "Alimentação", 
        //        "Beleza", "Educação", 
        //        "Lazer", "Saúde", "Transporte" 
        //    };

        //    foreach (string category in standardCategories)
        //        await AddCategoryAsync(category, false);
        //}

        //public async Task<List<Category>> ListCategoriesAsync(List<int> categoryIdsList)
        //{
        //    var categoriesList = await _categoryRepository
        //        .ListCategoriesAsync(categoryIdsList);

        //    if (categoriesList is null)
        //        return new List<Category>();

        //    return categoriesList;
        //}

        //public async Task<Category> GetByIdAsync(int categoryId)
        //{
        //    var result = await _categoryRepository.GetByIdAsync(categoryId);
        //    return result;
        //}

        //public async Task<List<Category>> ListStandardCategoriesAsync()
        //{
        //    var result = await _categoryRepository.ListStandardCategoriesAsync();
        //    if (result is null)
        //        Notification.RaiseError(Category.Error.CategoryNotFound);
            
        //    return result;
        //}

        //public async Task<Category> GetByNameAsync(string categoryName)
        //{
        //    return await _categoryRepository.GetByNameAsync(categoryName);
        //}
    }
}
