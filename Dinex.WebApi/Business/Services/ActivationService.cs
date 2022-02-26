namespace Dinex.WebApi.Business
{
    public class ActivationService : IActivationService
    {
        private readonly IActivationRepository _activationRepository;
        private readonly IUserService _userService;
        private readonly ISendMailService _sendMailService;
        public ActivationService(
            IActivationRepository activationRepository,
            IUserService userService,
            ISendMailService sendMailService
            )
        {
            _activationRepository = activationRepository;
            _userService = userService;
            _sendMailService = sendMailService;
        }

        public async Task ActivateAccount(string email, string activationCode)
        {
            var user = await _userService.GetByEmail(email);
            var listOfActivations = await _activationRepository.ListByUserIdAsync(user.Id);

            listOfActivations.RemoveAll(a => !a.ActivationCode.Equals(activationCode));
            if(listOfActivations.Count != 1)
            {
                //return error
            }

            const int activationExpiresInMinutes = 120;
            var createdAt = listOfActivations[0].CreatedAt;
            var currentTimeToExpire = DateTime.Now.AddMinutes(-activationExpiresInMinutes);
            if (currentTimeToExpire >= createdAt)
            {
                //return error
            }

            var updatedUser = await ActivateUser(user);

            // TODO: create standard categories here


        }

        public async Task<string> SendActivationCode(string email)
        {
            const int codeLength = 6;
            var user = await _userService.GetByEmail(email);
            var activationCode = GenerateActivatioCode(codeLength);
            var activationResult = await AddActivationOnDatabase(user.Id, activationCode);

            var sendResult = await _sendMailService.SendActivationCode(activationCode, user.FullName, user.Email);
            return sendResult;
        }

        private async Task<int> AddActivationOnDatabase(Guid userId, string activationCode)
        {
            var activation = new Activation();
            activation.UserId = userId;
            activation.ActivationCode = activationCode;
            activation.CreatedAt = DateTime.Now;

            var result = await _activationRepository.AddAsync(activation);
            return result;
        }

        private string GenerateActivatioCode(int codeLength)
        {
            var random = new Random();
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";
            const string chars = lower + upper + numbers;
            return new string(Enumerable.Repeat(chars, codeLength)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async Task<User> ActivateUser(User user)
        {
            user.IsActive = UserActivatioStatus.Active;
            var resultUser = await _userService.Update(user);
            resultUser.Password = null;
            return resultUser;
        }
    }
}
