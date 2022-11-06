namespace Dinex.Infra.Services
{
    public class GenerationCodeService : IGenerationCodeService
    {
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
    }
}
