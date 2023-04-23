namespace Dinex.Business
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<CategoryRequestDto, Category>().ReverseMap();
            CreateMap<CategoryResponseDto, Category>().ReverseMap();
        }
    }
}
