﻿namespace Dinex.WebApi.Infra
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<CategoryRequestModel, Category>().ReverseMap();
            CreateMap<CategoryResponseModel, Category>().ReverseMap();
        }
    }
}