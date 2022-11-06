namespace Dinex.Infra
{
    public interface IGenerationCodeService
    {
        string GenerateCode(int codeLength, CodeType generationOption = CodeType.Default);
    }
}
