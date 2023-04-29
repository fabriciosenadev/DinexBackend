﻿namespace Dinex.Business
{
    public class ActivationAccountManager : BaseService, IActivationAccountManager
    {
        private readonly IUserService _userService;
        private readonly IActivationService _activationService;
        private readonly ICategoryManager _categoryManager;
        private readonly IEmailService _emailService;
        private readonly ICodeManagerService _codeManagerService;
        public ActivationAccountManager(
            IUserService userService,
            IActivationService activationService,
            ICategoryManager categoryManager,
            IEmailService sendMailService,
            ICodeManagerService generationCodeService,
            IMapper mapper,
            INotificationService notification)
            : base(mapper, notification)
        {
            _userService = userService;
            _activationService = activationService;
            _categoryManager = categoryManager;
            _emailService = sendMailService;
            _codeManagerService = generationCodeService;
        }
        public async Task ActivateAccountAsync(string email, string activationCode)
        {
            var user = await _userService.GetByEmailAsync(email);

            await _activationService.ValidateActivationCode(activationCode, user.Id);

            await _categoryManager.BindStandardCategoriesAsync(user.Id);

            await _activationService.ClearActivationCodesAsync(user.Id);

            await _codeManagerService.ClearAllCodesByUserAsync(user.Id);

            await _userService.ActivateUserAsync(user);
        }

        public async Task<string> SendActivationCodeAsync(string email)
        {
            var activationCode = _codeManagerService.GenerateCode(DefaultCodeLength);

            var user = await _userService.GetByEmailAsync(email);

            //await _activationService.AddActivationOnDatabaseAsync(user.Id, activationCode);

            await _codeManagerService.AssignCodeToUserAsync(user.Id, activationCode, CodeReason.Activation);

            var sendEmailDto = new SendEmailDto { 
                EmailSubject = email,
                EmailTo = user.Email,
                FullName = user.FullName,
                EmailTemplateFileName = "activationAccount.html",
                GeneratedCode = activationCode,
                Origin = "activation",
                TemplateFieldToName = "{name}",
                TemplateFieldToUrl = "{activationUrl}"
            };

            var sendResult = await _emailService.SendByTemplateAsync(sendEmailDto);
            return sendResult;
        }
    }
}