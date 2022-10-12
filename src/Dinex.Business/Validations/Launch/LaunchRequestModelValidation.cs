namespace Dinex.Business
{
    public class LaunchAndPayMethodRequestModelValidation : AbstractValidator<LaunchAndPayMethodRequestDto>
    {
        public LaunchAndPayMethodRequestModelValidation()
        {
            ValidateLaunch();
            ValidatePayMethod();
        }

        private bool ForNotNullPayMethod(PayMethodFromLaunchRequestDto? payMethod)
        {
            var result = true;

            if(payMethod is not null && string.IsNullOrEmpty(payMethod?.PayMethod))
            {
                return false;
            }
            return result;
        }

        private void ValidateLaunch()
        {
            RuleFor(request => request.Launch.Date)
                .NotEmpty()
                .WithMessage("Informe a data do lançamento");

            RuleFor(request => request.Launch.CategoryId)
                .NotEmpty()
                .WithMessage("Informe a categoria do lançamento");

            RuleFor(request => request.Launch.Amount)
                .NotEmpty()
                .WithMessage("Informe o valor do lançamento");
        }

        public void ValidatePayMethod()
        {
            RuleFor(request => request.PayMethodFromLaunch)
                .Must(ForNotNullPayMethod)
                .WithMessage("Informe o metodo de pagamento");
        }
    }
}