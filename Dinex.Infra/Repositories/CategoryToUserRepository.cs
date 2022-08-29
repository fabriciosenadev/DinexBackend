namespace Dinex.Infra
{
    public class CategoryToUserRepository : Repository<CategoryToUser>, ICategoryToUserRepository
    {
        public CategoryToUserRepository(DinexBackendContext context) : base(context)
        {
        }

        public async Task<CategoryToUser> FindRelationAsync(int categoryId, Guid userId)
        {
            var result = await _context.CategoriesToUsers
                .Where(r => 
                    r.UserId.Equals(userId) && 
                    r.CategoryId.Equals(categoryId))
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<CategoryToUser> FindNotDeletedRelationAsync(int categoryId, Guid userId)
        {
            var result = await _context.CategoriesToUsers
                .Where(r =>
                    r.UserId.Equals(userId) &&
                    r.CategoryId.Equals(categoryId) &&
                    r.DeletedAt.Equals(null))
                .FirstOrDefaultAsync();

            return result;
        }
        
        public async Task<CategoryToUser> FindDeletedRelationAsync(int categoryId, Guid userId)
        {
            var result = await _context.CategoriesToUsers
                .Where(r => 
                    r.UserId.Equals(userId) && 
                    r.CategoryId.Equals(categoryId) && 
                    r.DeletedAt != null)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<CategoryToUser>> ListCategoryRelationIdsAsync(Guid userId)
        {
            var result = await _context.CategoriesToUsers
                .Where(r => r.UserId.Equals(userId) && r.DeletedAt.Equals(null))
                .ToListAsync();

            return result;
        }

        public async Task<List<CategoryToUser>> ListCategoryRelationIdsDeletedAsync(Guid userId)
        {
            var result = await _context.CategoriesToUsers
                .Where(r => r.UserId.Equals(userId) && r.DeletedAt != null)
                .ToListAsync();

            return result;
        }
    }
}
