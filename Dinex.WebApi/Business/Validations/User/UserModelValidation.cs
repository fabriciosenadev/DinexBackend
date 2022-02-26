namespace Dinex.WebApi.Business
{
    public class UserModelValidation : AbstractValidator<UserInputModel>
    {
        private readonly IUserService _userService;

        public UserModelValidation(IUserService userService)
        {
            _userService = userService;

            ValidateName();

            ValidateEmail();

            VidatePassword();
        }
        private bool UniqueEmail(string email)
        {
            var hasAlreadyExists = _userService.GetByEmail(email);
            if (!(hasAlreadyExists.Result == null)) return false;

            return true;
        }

        private void VidatePassword()
        {
            RuleFor(u => u.Password)
                .NotEmpty()
                .WithName("Senha")
                .WithMessage("Preencha a senha");

            RuleFor(u => u.ConfirmPassword)
                .Equal(u => u.Password)
                .WithMessage("Senhas devem ser iguais");
        }

        private void ValidateEmail()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress()
                .WithName("E-mail")
                .WithMessage("Informe seu melhor e-mail")
                .Must(UniqueEmail)
                .WithMessage("Utilize outro endereço de e-mail.");
        }

        private void ValidateName()
        {
            RuleFor(u => u.FullName)
                .MinimumLength(3)
                .WithName("Nome completo")
                .WithMessage("Informe seu nome completo");
        }

    }
}
