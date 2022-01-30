namespace Dinex.WebApi.Infra
{
    public class LoginMapper : Profile
    {
        public LoginMapper()
        {
            CreateMap<AuthenticationRequest, Login>();
        }
    }
}
