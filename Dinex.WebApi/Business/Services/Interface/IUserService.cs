﻿namespace Dinex.WebApi.Business
{
    public interface IUserService
    {
        /// <summary>
        /// adiciona novo usuario
        /// </summary>
        /// <param name="user">dados do usuario</param>
        /// <returns>dados do usuario</returns>
        Task<int> Create(User user);

        /// <summary>
        /// busca usuario por codigo identificador
        /// </summary>
        /// <param name="id">codigo identificador</param>
        /// <returns>dados do usuario</returns>
        Task<User> GetById(Guid id);
        
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
        /// <returns>dados do usuario atualizados</returns>
        Task<User> Update(User user);

        /// <summary>
        /// obtem o usuario do contexto http passado
        /// </summary>
        /// <param name="httpContext">contexto http</param>
        /// <returns>o usuario inserido no contexto http</returns>
        Task<User> GetFromContext(HttpContext httpContext);
    }
}
