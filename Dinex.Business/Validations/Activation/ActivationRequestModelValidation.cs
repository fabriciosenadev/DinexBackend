namespace Dinex.Business
{
    public class ActivationRequestModelValidation : AbstractValidator<ActivationRequestModel>
    {
        public ActivationRequestModelValidation()
        {
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
            RuleFor(a => a.ActivationCode)
                .NotEmpty()
                .WithName("Código de ativação")
                .WithMessage("Código de ativação deve ser informnado");
        }
    }
}
