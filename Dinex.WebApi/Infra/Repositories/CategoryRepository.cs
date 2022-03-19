namespace Dinex.WebApi.Infra
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DinexBackendContext context) : base(context)
        {
        }

        public async Task<List<Category>> ListStandardCategories()
        {
            var result = await _context.Categories.Where(c => c.IsCustom.Equals(false)).ToListAsync();
            return result;
        }
    }
}
