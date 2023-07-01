using Application.Commons;
using Application.ViewModels;
using Application.ViewModels.FilterModels;
using Application.ViewModels.UserViewModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IBaseUserService
    {
        Task<bool> AddAsync(BaseUser batch);
        Task<Pagination<BaseUser>> GetAllAsync(int pageIndex, int pageSize);
        Task<BaseUser?> GetByIdAsync(Guid entityId);
        Task<int> GetCountAsync();
        Task<Pagination<BaseUser>> GetFilterAsync(UserFilteringModel entity,int pageIndex,int pageSize);
        Task<UserLoginDTOResponse> LoginAsync(UserLoginDTO loginObject);
        Task<UserToken> RefreshToken(string accessToken, string refreshToken);
        bool Remove(Guid entityId);
        bool Update(BaseUser entity);
    }
}
