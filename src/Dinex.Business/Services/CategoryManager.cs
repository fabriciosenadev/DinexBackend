using System.Collections.Generic;

namespace Dinex.Business
{
    public class CategoryManager : BaseService, ICategoryManager
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryToUserService _categoryToUserService;
        private readonly ILaunchService _launchService;
        private readonly ICategoryToUserRepository _categoryToUserRepository;
        public CategoryManager(
            ICategoryService categoryService,
            ICategoryToUserService categoryToUserService,
            IMapper mapper,
            ILaunchService launchService,
            INotificationService notification,
            ICategoryToUserRepository categoryToUserRepository)
            : base(mapper, notification)
        {
            _categoryService = categoryService;
            _categoryToUserService = categoryToUserService;
            _launchService = launchService;
            _categoryToUserRepository = categoryToUserRepository;
        }

        private Applicable StringToEnum(string applicable)
        {
            return Enum.Parse<Applicable>(CapitalizeFirstLetter(applicable));
        }

        private string CapitalizeFirstLetter(string value)
        {
            var newStr = char.ToUpper(value[0]) + value.Substring(1);
            return newStr;
        }

        private List<CategoryResponseDto> FillApplicableToToCategoriesReponse(
            List<CategoryResponseDto> categoriesResponse, List<CategoryToUser> categoriesToUser)
        {
            var categoriesResult = new List<CategoryResponseDto>();
            categoriesResponse.ForEach(category =>
            {
                var applicable = categoriesToUser
                    .Where(x => x.CategoryId == category.Id)
                    .Select(x => x.Applicable);

                categoriesResult.Add(new CategoryResponseDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    IsCustom = category.IsCustom,
                    CreatedAt = category.CreatedAt,
                    Applicable = applicable.First().ToString()
                });
            });
            return categoriesResult;
        }

        public async Task BindStandardCategoriesAsync(Guid userId)
        {
            var standardCategories = await _categoryService.ListStandardCategoriesAsync();
            if (standardCategories.Count == 0)
            {
                await _categoryService.AddStandardCategoriesAsync();
                standardCategories = await _categoryService.ListStandardCategoriesAsync();
            }

            await _categoryToUserService.AssignStandardCategoriesToUserAsync(userId, standardCategories);
        }

        public async Task<CategoryResponseDto> CreateAsync(CategoryRequestDto request, Guid userId, string applicable)
        {
            var category = _mapper.Map<Category>(request);

            var resultCreation = await _categoryService.CreateCategoryAsync(category);

            await _categoryToUserService.CheckExistsCategoryRelationToUser(resultCreation.Id, userId);

            var applicableEnum = StringToEnum(applicable);

            await _categoryToUserService.AssignCategoryToUserAsync(userId, resultCreation.Id, applicableEnum);

            var categoryResult = _mapper.Map<CategoryResponseDto>(resultCreation);
            categoryResult.Applicable = applicable;
            return categoryResult;
        }

        public async Task DeleteAsync(int categoryId, Guid userId)
        {
            _categoryService.ValidateCategoryId(categoryId);

            var count = await _launchService.CountByCategoryIdAsync(categoryId, userId);
            if (count > 0)
                Notification.RaiseError(Category.Error.CategoryInUse);

            await _categoryToUserService.CheckNotExistsCategoryRelationToUser(categoryId, userId);

            var relation = await _categoryToUserService.GetRelationAsync(categoryId, userId);

            await _categoryToUserService.SoftDeleteRelationAsync(relation);
        }

        public async Task<CategoryResponseDto> RestoreDeletedCategoryAsync(Guid userId, int categoryId)
        {
            var category = await _categoryService.GetByIdAsync(categoryId);

            await _categoryToUserService.RestoreDeletedCategoryAsync(userId, categoryId);

            var categoryResult = _mapper.Map<CategoryResponseDto>(category);
            return categoryResult;
        }

        public async Task<CategoryResponseDto> GetCategoryAsync(int categoryId, Guid userId)
        {
            _categoryService.ValidateCategoryId(categoryId);

            await _categoryToUserService.CheckNotExistsCategoryRelationToUser(categoryId, userId);

            var category = await _categoryService.GetByIdAsync(categoryId);

            var categoryResult = _mapper.Map<CategoryResponseDto>(category);
            return categoryResult;
        }

        public async Task<List<CategoryResponseDto>> ListCategoriesAsync(Guid userId, bool isToListDeleted)
        {

            var listCategoryRelations = await _categoryToUserService
                .ListCategoryRelationIdsAsync(userId, isToListDeleted);

            if (listCategoryRelations is null)
                return new List<CategoryResponseDto>();

            var categoryIdsList = _categoryToUserService.ListCategoryIds(listCategoryRelations);
            var categoriesList = await _categoryService.ListCategoriesAsync(categoryIdsList);

            var listResultModel = _mapper.Map<List<CategoryResponseDto>>(categoriesList);

            var listResult = FillApplicableToToCategoriesReponse(
                listResultModel, listCategoryRelations);
            return listResult;
        }
    }
}
