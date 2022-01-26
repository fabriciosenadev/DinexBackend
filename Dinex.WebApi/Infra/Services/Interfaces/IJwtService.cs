namespace Dinex.WebApi.Infra
{
    public interface IJwtService
    {
       string GenerateToken(User user);
    }
}
