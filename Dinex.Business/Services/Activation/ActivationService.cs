namespace Dinex.Business
{
    public class ActivationService : BaseService, IActivationService
    {
        const int MaxActivationCodesAllowed = 1;

        private readonly IActivationRepository _activationRepository;      

        public ActivationService(
            IActivationRepository activationRepository, 
            IMapper mapper,
            INotificationService notification)
            : base(mapper, notification)
        {
            _activationRepository = activationRepository;
        }

        public async Task ValidateActivationCode(string activationCode, Guid userId)
        {
            var listOfActivations = await _activationRepository.ListByUserIdAsync(userId);

            listOfActivations.RemoveAll(a => !a.ActivationCode.Equals(activationCode));
            if (listOfActivations.Count != MaxActivationCodesAllowed)
                Notification.RaiseError(Activation.Error.ActivationInvalidCode);

            const int activationExpiresInMinutes = 120;
            var createdAt = listOfActivations[0].CreatedAt;
            var currentTimeToExpire = DateTime.Now.AddMinutes(-activationExpiresInMinutes);
            if (currentTimeToExpire >= createdAt)
                Notification.RaiseError(Activation.Error.ActivationExpiredCode);
        }

        public async Task ClearActivationCodesAsync(Guid userId)
        {
            await _activationRepository.DeleteByUserIdAsync(userId);
        }

        public string GenerateActivatioCodeAsync(int codeLength)
        {
            var random = new Random();
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";
            const string chars = lower + upper + numbers;
            return new string(Enumerable.Repeat(chars, codeLength)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public async Task<int> AddActivationOnDatabaseAsync(Guid userId, string activationCode)
        {
            var activation = new Activation();
            activation.UserId = userId;
            activation.ActivationCode = activationCode;
            activation.CreatedAt = DateTime.Now;

            var result = await _activationRepository.AddAsync(activation);
            return result;
        }
    }
}
