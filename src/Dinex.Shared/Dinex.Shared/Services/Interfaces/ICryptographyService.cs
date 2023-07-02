namespace Dinex.Shared
{
    public interface ICryptographyService
    {
        string Encrypt(string value);
        bool CompareValues(string encryptedValue, string valueToCompare);
    }
}
