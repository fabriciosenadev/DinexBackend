namespace Dinex.Business
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryToUserService _categoryToUserService;
        private readonly IMapper _mapper;

        public CategoryService(
            ICategoryRepository categoryRepository,
            ICategoryToUserService categoryToUserService
,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _categoryToUserService = categoryToUserService;
            _mapper = mapper;
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

        private async Task<CategoryToUser> GetCategoryToUserRelation(int categoryId, Guid userId)
        {
            var result = await _categoryToUserService.GetRelationAsync(categoryId, userId);
            return result;
        }

        private List<CategoryResponseDto> FillApplicableToToCategoriesReponse(List<CategoryResponseDto> categoriesResponse, List<CategoryToUser> categoriesToUser)
        {
            categoriesResponse.ForEach(category =>
            {
                var applicable = categoriesToUser
                    .Where(x => x.CategoryId == category.Id)
                    .Select(x => x.Applicable);

                category.Applicable = applicable.ElementAt(0).ToString();
            });
            return categoriesResponse;
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

        public async Task<CategoryResponseDto> CreateAsync(CategoryRequestDto request, Guid userId, string applicable)
        {
            var category = _mapper.Map<Category>(request);

            var foundCategory = await _categoryRepository.GetByNameAsync(category.Name);

            Category result = foundCategory;
            if (foundCategory is null)
                result = await AddCategoryAsync(category.Name, true);

            var hasRelation = await GetCategoryToUserRelation(result.Id, userId);
            if (hasRelation is not null)
            {
                // msg: Category already exists
                throw new AppException(Category.Error.CategoryAlreadyExists.ToString());
            }

            var applicableEnum = EnumConvertion
                .StringToEnum<Applicable>(_categoryToUserService.CapitalizeFirstLetter(applicable));

            await AssignCategoryToUserAsync(userId, result.Id, applicableEnum);

            var categoryResult = _mapper.Map<CategoryResponseDto>(result);
            categoryResult.Applicable = applicable;
            return categoryResult;
        }

        public async Task DeleteAsync(int categoryId, Guid userId)
        {
            if (categoryId == 0)
            {
                // msg: Category not provided
                throw new InfraException(Category.Error.CategoryNotProvided.ToString());
            }

            var relation = await _categoryToUserService.GetRelationAsync(categoryId, userId);
            if (relation is null)
            {
                // msg: Category not found
                throw new AppException(Category.Error.CategoryNotFound.ToString());
            }

            await _categoryToUserService.SoftDeleteRelationAsync(relation);
        }

        public async Task<CategoryResponseDto> GetCategoryAsync(int categoryId, Guid userId)
        {
            if (categoryId == 0)
            {
                // msg: Category not provided
                throw new InfraException(Category.Error.CategoryNotProvided.ToString());
            }

            var relation = await _categoryToUserService.GetRelationAsync(categoryId, userId);
            if (relation is null)
            {
                // msg: Category not found
                throw new AppException(Category.Error.CategoryNotFound.ToString());
            }

            var category = await _categoryRepository.GetByIdAsync(categoryId);

            var categoryResult = _mapper.Map<CategoryResponseDto>(category);
            return categoryResult;
        }

        public async Task<List<CategoryResponseDto>> ListCategoriesAsync(Guid userId)
        {
            var listCategoryRelations = await _categoryToUserService.ListCategoryRelationIdsAsync(userId);

            if (listCategoryRelations is null)
                return new List<CategoryResponseDto>();

            var listCategories = await _categoryRepository
                .ListCategoriesAsync(_categoryToUserService.ListCategoryIds(listCategoryRelations));

            if (listCategories is null)
                return new List<CategoryResponseDto>();

            var listResultModel = _mapper.Map<List<CategoryResponseDto>>(listCategories);

            var listResult = FillApplicableToToCategoriesReponse(listResultModel, listCategoryRelations);
            return listResult;
        }

        public async Task<List<CategoryResponseDto>> ListCategoriesDeletedAsync(Guid userId)
        {
            var listCategoryRelations = await _categoryToUserService.ListCategoryRelationIdsDeletedAsync(userId);

            if (listCategoryRelations is null)
                return new List<CategoryResponseDto>();

            var listCategories = await _categoryRepository
                .ListCategoriesAsync(_categoryToUserService.ListCategoryIds(listCategoryRelations));

            if (listCategories is null)
                return new List<CategoryResponseDto>();

            var listResultModel = _mapper.Map<List<CategoryResponseDto>>(listCategories);

            var listResult = FillApplicableToToCategoriesReponse(listResultModel, listCategoryRelations);

            return listResult;
        }

        public async Task<CategoryResponseDto> RestoreDeletedCategoryAsync(Guid userId, int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category is null)
            {
                // msg: Category not found
                throw new AppException(Category.Error.CategoryNotFound.ToString());
            }

            var relationResult = await _categoryToUserService.RestoreDeletedCategoryAsync(userId, categoryId);

            var categoryResult = _mapper.Map<CategoryResponseDto>(category);
            return categoryResult;
        }
    }
}
