namespace Dinex.Business
{
    public class ActivationManager : IActivationManager
    {
        private readonly IUserService _userService;
        private readonly IActivationService _activationService;
        private readonly ICategoryManager _categoryManager;
        private readonly ISendMailService _sendMailService;
        public ActivationManager(
            IUserService userService,
            IActivationService activationService,
            ICategoryManager categoryManager,
            ISendMailService sendMailService)
        {
            _userService = userService;
            _activationService = activationService;
            _categoryManager = categoryManager;
            _sendMailService = sendMailService;
        }
        public async Task ActivateAccountAsync(string email, string activationCode)
        {
            var user = await _userService.GetByEmailAsync(email);

            await _activationService.ValidateActivationCode(activationCode, user.Id);

            await _categoryManager.BindStandardCategoriesAsync(user.Id);

            await _activationService.ClearActivationCodesAsync(user.Id);

            await _userService.ActivateUserAsync(user);
        }

        public async Task<string> SendActivationCodeAsync(string email)
        {
            const int codeLength = 32;
            var activationCode = _activationService.GenerateActivatioCodeAsync(codeLength);

            var user = await _userService.GetByEmailAsync(email);

            await _activationService.AddActivationOnDatabaseAsync(user.Id, activationCode);

            var sendResult = await _sendMailService.SendActivationCodeAsync(activationCode, user.FullName, user.Email);
            return sendResult;
        }
    }
}
