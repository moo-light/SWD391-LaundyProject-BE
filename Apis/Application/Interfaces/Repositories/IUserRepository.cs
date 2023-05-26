﻿using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task<User> GetUserByUserNameAndPasswordHash(string username, string passwordHash);

        Task<bool> CheckEmailExisted(string username);
    }
}
