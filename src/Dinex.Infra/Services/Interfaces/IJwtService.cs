namespace Dinex.Infra
{
    public interface IJwtService
    {
       string GenerateToken(User user);
    }
}
