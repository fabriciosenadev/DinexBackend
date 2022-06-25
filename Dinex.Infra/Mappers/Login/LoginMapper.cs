namespace Dinex.Infra
{
    public class LoginMapper : Profile
    {
        public LoginMapper()
        {
            CreateMap<AuthenticationRequestModel, Login>();
        }
    }
}
