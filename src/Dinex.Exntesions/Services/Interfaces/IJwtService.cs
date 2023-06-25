namespace Dinex.Extensions
{
    public interface IJwtService
    {
       string GenerateToken(User user);
    }
}
