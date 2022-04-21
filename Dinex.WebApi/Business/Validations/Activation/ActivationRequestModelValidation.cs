namespace Dinex.WebApi.Business
{
    public class ActivationRequestModelValidation : AbstractValidator<ActivationRequestModel>
    {
        public ActivationRequestModelValidation()
        {
            ValidateEmail();
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
            RuleFor(a => !string.IsNullOrEmpty(a.ActivationCode));
        }
    }
}
