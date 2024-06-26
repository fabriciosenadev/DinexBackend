﻿namespace Dinex.Business
{
    public class CodeManagerService : BaseService, ICodeManagerService
    {
        const int MaxActivationCodesAllowed = 1;
        private readonly ICodeManagerRepository _codeManagerRepository;

        public CodeManagerService(
            IMapper mapper,
            INotificationService notification,
            ICodeManagerRepository codeManagerRepository)
            : base(mapper, notification)
        {
            _codeManagerRepository = codeManagerRepository;
        }


        public string GenerateCode(int codeLength, CodeType generationOption = CodeType.Default)
        {
            var random = new Random();
            var chars = string.Empty;

            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";

            switch (generationOption)
            {
                case CodeType.JustLower:
                    chars = lower;
                    break;
                case CodeType.JustUpper:
                    chars = upper;
                    break;
                case CodeType.JustNumbers:
                    chars = numbers;
                    break;
                case CodeType.LowerAndUpper:
                    chars = lower + upper;
                    break;
                case CodeType.LowerAndNumbers:
                    chars = lower + numbers;
                    break;
                case CodeType.UpperAndNumbers:
                    chars = upper + numbers;
                    break;
                default:
                    chars = lower + upper + numbers;
                    break;
            }

            return new string(Enumerable.Repeat(chars, codeLength)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<int> AssignCodeToUserAsync(Guid userId, string code, CodeReason codeReason)
        {
            var codeManager = new CodeManager
            {
                Code = code,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                Reason = codeReason
            };

            var result = await _codeManagerRepository.CreateAsync(codeManager);
            return result;
        }

        public async Task ClearAllCodesByUserAsync(Guid userId, CodeReason codeReason)
        {
            await _codeManagerRepository.DeleteByUserIdAsync(userId, codeReason);
        }

        public Task ClearAllCodesByUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task ValidateActivationCode(string activationCode, Guid userId)
        {
            var listOfActivations = await _codeManagerRepository.ListByUserIdAsync(userId);

            listOfActivations.RemoveAll(a => !a.Code.Equals(activationCode));
            if (listOfActivations.Count != MaxActivationCodesAllowed)
                Notification.RaiseError(CodeManager.Error.ActivationInvalidCode);

            const int activationExpiresInMinutes = 120;
            var createdAt = listOfActivations[0].CreatedAt;
            var currentTimeToExpire = DateTime.Now.AddMinutes(-activationExpiresInMinutes);
            if (currentTimeToExpire >= createdAt)
                Notification.RaiseError(CodeManager.Error.ActivationExpiredCode);
        }
    }
}
