namespace Dinex.WebApi.Business
{
    public interface IUserService
    {
        /// <summary>
        /// adiciona novo usuario
        /// </summary>
        /// <param name="user">dados do usuario</param>
        /// <returns>dados do usuario</returns>
        Task<int> CreateAsync(User user);

        /// <summary>
        /// busca usuario por codigo identificador
        /// </summary>
        /// <param name="id">codigo identificador</param>
        /// <returns>dados do usuario</returns>
        Task<User> GetByIdAsync(Guid id);

        /// <summary>
        /// busca usuario por email
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>dados do usuario</returns>
        Task<User> GetByEmailAsync(string email);

        /// <summary>
        /// busca usuario por codigo identificador
        /// </summary>
        /// <param name="id">codigo identificador</param>
        /// <returns>dados do usuario</returns>
        Task<User> GetByIdAsNoTracking(Guid id);

        /// <summary>
        /// atualiza usuario existente
        /// </summary>
        /// <param name="user">dados do usuario</param>
        /// <param name="needUpdatePassword">avalia necessidade de atualizar a senha</param>
        /// <returns>dados do usuario atualizados</returns>
        Task<User> UpdateAsync(User user, bool needUpdatePassword);

        /// <summary>
        /// obtem o usuario do contexto http passado
        /// </summary>
        /// <param name="httpContext">contexto http</param>
        /// <returns>o usuario inserido no contexto http</returns>
        Task<User> GetFromContextAsync(HttpContext httpContext);
    }
}
