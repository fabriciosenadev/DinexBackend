namespace Dinex.WebApi.Infra
{
    public class CategoryToUserRepository : Repository<CategoryToUser>, ICategoryToUserRepository
    {
        public CategoryToUserRepository(DinexBackendContext context) : base(context)
        {
        }

        public async Task<CategoryToUser> FindRelationAsync(int categoryId, Guid userId)
        {
            var result = await _context.CategoryiesToUsers
                .Where(r => 
                    r.UserId.Equals(userId) && 
                    r.CategoryId.Equals(categoryId) && 
                    r.DeletedAt.Equals(null))
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<int>> ListCategoryRelationIdsAsync(Guid userId)
        {
            var result = await _context.CategoryiesToUsers
                .Where(r => r.UserId.Equals(userId) && r.DeletedAt.Equals(null))
                .Select(r => r.CategoryId)
                .ToListAsync();

            return result;
        }
    }
}
