namespace Dinex.Business
{
    public class CategoryToUserService : BaseService, ICategoryToUserService
    {
        private readonly ICategoryToUserRepository _categoryToUserRepository;

        public CategoryToUserService(
            ICategoryToUserRepository categoryToUserRepository,
            IMapper mapper,
            INotificationService notification)
            : base(mapper, notification)
        {
            _categoryToUserRepository = categoryToUserRepository;
        }

        //public async Task AssignCategoryToUserAsync(Guid userId, int categoryId, Applicable applicable)
        //{
        //    var relation = new CategoryToUser();
        //    relation.UserId = userId;
        //    relation.CategoryId = categoryId;
        //    relation.Applicable = applicable;
        //    relation.CreatedAt = DateTime.Now;

        //    await _categoryToUserRepository.AddAsync(relation);
        //}

        //public async Task AssignStandardCategoriesToUserAsync(Guid userId, List<Category> standardCategories)
        //{
        //    foreach (var category in standardCategories)
        //    {
        //        if (category.Name == "Salário")
        //            await AssignCategoryToUserAsync(userId, category.Id, Applicable.In);
        //        else
        //            await AssignCategoryToUserAsync(userId, category.Id, Applicable.Out);
        //    }
        //}

        //public async Task SoftDeleteRelationAsync(CategoryToUser categoryToUser)
        //{
        //    categoryToUser.DeletedAt = DateTime.Now;
        //    var result = await _categoryToUserRepository.UpdateAsync(categoryToUser);
        //    if (result != Success)
        //        Notification.RaiseError(CategoryToUser.Error.CategoryRelationFailToCreate);
        //}

        //public async Task<CategoryToUser> GetRelationAsync(int categoryId, Guid userId)
        //{
        //    var result = await _categoryToUserRepository.FindNotDeletedRelationAsync(categoryId, userId);
        //    return result;
        //}

        //public async Task<List<CategoryToUser>> ListCategoryRelationIdsAsync(Guid userId, bool isToListDeleted)
        //{
        //    if (!isToListDeleted)
        //        return await _categoryToUserRepository.ListCategoryRelationIdsAsync(userId);
        //    else
        //        return await _categoryToUserRepository.ListCategoryRelationIdsDeletedAsync(userId);
        //}

        //public async Task<CategoryToUser> RestoreDeletedCategoryAsync(Guid userId, int categoryId)
        //{
        //    var relation = await _categoryToUserRepository.FindDeletedRelationAsync(categoryId, userId);
        //    if (relation is null)
        //        Notification.RaiseError(CategoryToUser.Error.CategoryRelationNotFound);

        //    relation.DeletedAt = null;
        //    await _categoryToUserRepository.UpdateAsync(relation);
        //    return relation;
        //}

        //public List<int> ListCategoryIds(List<CategoryToUser> categoryToUsers)
        //    => categoryToUsers.Select(x => x.CategoryId).ToList();

        //public async Task CheckExistsCategoryRelationToUser(int categoryId, Guid userId)
        //{
        //    var categoryToUser = await _categoryToUserRepository.FindRelationAsync(categoryId, userId);
        //    if (categoryToUser is not null)
        //        Notification.RaiseError(CategoryToUser.Error.CategoryRelationAlreadyExists);
        //}

        //public async Task CheckNotExistsCategoryRelationToUser(int categoryId, Guid userId)
        //{
        //    var relation = await GetRelationAsync(categoryId, userId);
        //    if (relation is null)
        //        Notification.RaiseError(CategoryToUser.Error.CategoryRelationNotFound);
        //}
    }
}
