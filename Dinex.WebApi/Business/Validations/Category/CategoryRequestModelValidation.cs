﻿namespace Dinex.WebApi.Business
{
    public class CategoryRequestModelValidation : AbstractValidator<CategoryRequestModel>
    {
        public CategoryRequestModelValidation()
        {
            ValidateName();
        }

        //private bool UniqueCategory(string categoryName)
        //{
        //    var hasAlreadyExists = _categoryService.GetByName(categoryName);
        //    if (!(hasAlreadyExists.Result == null)) return false;

        //    return true;
        //}

        private void ValidateName()
        {
            RuleFor(c => c.Name)
                .MinimumLength(3)
                .WithName("Nome muito curto");
        }
    }
}