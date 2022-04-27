namespace Dinex.WebApi.Infra
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DinexBackendContext context) : base(context)
        {
        }

        public async Task<List<Category>> ListStandardCategoriesAsync()
        {
            var result = await _context.Categories.Where(c => c.IsCustom.Equals(false)).ToListAsync();
            return result;
        }

        public async Task<Category> GetByNameAsync(string categoryName)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(c => c.Name.Equals(categoryName));
            return result;
        }

        public async Task<Category> GetByIdAsync(int categoryId)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(c => c.Id.Equals(categoryId));
            return result;
        }

        public async Task<List<Category>> ListCategoriesAsync(List<int> listCategoryRelationIds)
        {
            var result = await _context.Categories
                .Where(c => listCategoryRelationIds.Contains(c.Id))
                .ToListAsync();
            return result;
        }
    }
}
