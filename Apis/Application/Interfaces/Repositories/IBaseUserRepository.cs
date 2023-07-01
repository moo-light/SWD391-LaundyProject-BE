using Application.ViewModels.FilterModels;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IBaseUserRepository:IGenericRepository<BaseUser>
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="email"></param>
    /// <param name="passwordHash"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    Task<BaseUser?> GetUserByEmailAndPasswordHash(string email, string password);

    Task<bool> CheckEmailExisted(string username);
    IEnumerable<BaseUser> GetFilter(UserFilteringModel? entity);
}