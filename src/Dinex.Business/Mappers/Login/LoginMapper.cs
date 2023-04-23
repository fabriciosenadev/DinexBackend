namespace Dinex.Business
{
    public class LoginMapper : Profile
    {
        public LoginMapper()
        {
            CreateMap<AuthenticationRequestDto, Login>();
        }
    }
}
