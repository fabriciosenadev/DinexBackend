namespace Dinex.WebApi.Infra
{
    public class CryptographyService : ICryptographyService
    {
        public string Encrypt(string value)
        {
            var result = BCrypt.Net.BCrypt.HashPassword(value);
            return result;
        }

        public bool CompareValues(string encryptedValue, string valueToCompare)
        {
            var result = BCrypt.Net.BCrypt.Verify(valueToCompare, encryptedValue);
            return result;
        }
    }
}
