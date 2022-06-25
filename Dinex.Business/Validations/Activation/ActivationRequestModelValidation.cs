namespace Dinex.Business
{
    public class ActivationRequestModelValidation : AbstractValidator<ActivationRequestDto>
    {
        private readonly IActionContextAccessor _actionContextAccessor;

        public ActivationRequestModelValidation(IActionContextAccessor actionContextAccessor)
        {
            _actionContextAccessor = actionContextAccessor;
            ValidateEmail();
            ValidateActivationCode();
        }

        private void ValidateEmail()
        {
            RuleFor(a => a.Email)
                .NotEmpty()
                .EmailAddress()
                .WithName("E-mail")
                .WithMessage("Informe seu e-mail cadastrado.");
        }

        private void ValidateActivationCode()
        {
            var requestOrigin = _actionContextAccessor.ActionContext?.ActionDescriptor.DisplayName;
            if(!string.IsNullOrEmpty(requestOrigin) && requestOrigin.Contains("ActivateAccount"))
                RuleFor(a => a.ActivationCode)
                    .NotEmpty()
                    .WithName("Código de ativação")
                    .WithMessage("Código de ativação deve ser informnado");

        }
    }
}
