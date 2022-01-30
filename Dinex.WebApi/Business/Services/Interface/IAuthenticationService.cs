namespace Dinex.WebApi.Business
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Autenticacao de usuario
        /// </summary>
        /// <param name="loginData">dados de login</param>
        /// <returns></returns>
        Task<(User, string)> Authenticate(Login loginData);
    }
}
