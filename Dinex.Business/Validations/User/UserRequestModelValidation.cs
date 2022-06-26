namespace Dinex.Business
{
    public class UserRequestModelValidation : AbstractValidator<UserRequestDto>
    {
        private readonly IUserService _userService;
        private readonly IActionContextAccessor _actionContextAccessor;

        public UserRequestModelValidation(IUserService userService, IActionContextAccessor actionContextAccessor)
        {
            _userService = userService;
            _actionContextAccessor = actionContextAccessor;

            ValidateName();

            ValidateEmail();

            VidatePassword();
        }
        private bool UniqueEmailWhenNewUser(string email)
        {
            var result = true;

            var requestMethod = _actionContextAccessor.ActionContext?.HttpContext.Request.Method;

            var hasAlreadyExists = _userService.GetByEmailAsync(email);

            if (!String.IsNullOrEmpty(requestMethod))
            {
                if (requestMethod.Equals("POST"))
                {
                    if (hasAlreadyExists.Result is not null)
                        result = false;
                }
                else
                {
                    if (hasAlreadyExists.Result is null)
                        result = false;
                }
            }

            return result;
        }

        private void VidatePassword()
        {
            RuleFor(u => u.Password)
                .NotEmpty()
                .MinimumLength(8)
                .WithMessage("Senha minima, 8 caracteres")
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
                .Must(UniqueEmailWhenNewUser)
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
