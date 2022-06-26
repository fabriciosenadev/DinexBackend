namespace Dinex.Business
{
    public class ActivationService : IActivationService
    {
        private readonly IActivationRepository _activationRepository;
        private readonly IUserService _userService;
        private readonly ISendMailService _sendMailService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ActivationService(
            IActivationRepository activationRepository,
            IUserService userService,
            ISendMailService sendMailService,
            ICategoryService categoryService
,
            IMapper mapper)
        {
            _activationRepository = activationRepository;
            _userService = userService;
            _sendMailService = sendMailService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<ActivationReason> ActivateAccountAsync(string email, string activationCode)
        {
            var user = await _userService.GetByEmailAsync(email);
            var listOfActivations = await _activationRepository.ListByUserIdAsync(user.Id);

            listOfActivations.RemoveAll(a => !a.ActivationCode.Equals(activationCode));
            if(listOfActivations.Count != 1)
            {
                return ActivationReason.InvalidCode;
            }

            const int activationExpiresInMinutes = 120;
            var createdAt = listOfActivations[0].CreatedAt;
            var currentTimeToExpire = DateTime.Now.AddMinutes(-activationExpiresInMinutes);
            if (currentTimeToExpire >= createdAt)
            {
                return ActivationReason.ExpiredCode;
            }

            await _categoryService.BindStandardCategoriesAsync(user.Id);

            await ClearActivationCodesAsync(user.Id);

            await ActivateUserAsync(user);

            return ActivationReason.Success;
        }

        public async Task<string> SendActivationCodeAsync(string email)
        {
            const int codeLength = 32;
            var activationCode = GenerateActivatioCodeAsync(codeLength);

            var user = await _userService.GetByEmailAsync(email);

            await AddActivationOnDatabaseAsync(user.Id, activationCode);

            var sendResult = await _sendMailService.SendActivationCodeAsync(activationCode, user.FullName, user.Email);
            return sendResult;
        }

        private async Task<int> AddActivationOnDatabaseAsync(Guid userId, string activationCode)
        {
            var activation = new Activation();
            activation.UserId = userId;
            activation.ActivationCode = activationCode;
            activation.CreatedAt = DateTime.Now;

            var result = await _activationRepository.AddAsync(activation);
            return result;
        }

        private string GenerateActivatioCodeAsync(int codeLength)
        {
            var random = new Random();
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";
            const string chars = lower + upper + numbers;
            return new string(Enumerable.Repeat(chars, codeLength)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async Task ActivateUserAsync(User user)
        {
            user.IsActive = UserActivatioStatus.Active;
            user.UpdatedAt = DateTime.Now;

            var userDto = _mapper.Map<UserRequestDto>(user);

            var resultUser = await _userService
                .UpdateAsync(userDto, false, user.Id);
        }

        private async Task ClearActivationCodesAsync(Guid userId)
        {
            await _activationRepository.DeleteByUserIdAsync(userId);
        }
    }
}
