namespace Dinex.WebApi.Infra
{
    public class LoginMapper : Profile
    {
        public LoginMapper()
        {
            CreateMap<AuthenticationRequestModel, Login>();
        }
    }
}
