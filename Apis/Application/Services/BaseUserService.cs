using Application.Commons;
using Application.ViewModels.FilterModels;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Utils;
using Application.ViewModels;
using Application.ViewModels.UserViewModels;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Application.Services
{
    public class BaseUserService : IBaseUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppConfiguration _configuration;
        private readonly ICurrentTime _currentTime;

        public BaseUserService(IUnitOfWork unitOfWork,AppConfiguration configuration ,ICurrentTime currentTime)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _currentTime = currentTime;
        }

        public async Task<Pagination<BaseUser>> GetAllAsync(int pageIndex,int pageSize)
        {
            var baseUsers = await _unitOfWork.UserRepository.ToPagination(pageIndex,pageSize);
            return baseUsers;
        }

        public async Task<BaseUser?> GetByIdAsync(Guid entityId) => await _unitOfWork.UserRepository.GetByIdAsync(entityId);
        public async Task<bool> AddAsync(BaseUser store)
        {
            await _unitOfWork.UserRepository.AddAsync(store);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public bool Remove(Guid entityId)
        {
            _unitOfWork.UserRepository.SoftRemoveByID(entityId);
            return _unitOfWork.SaveChange() > 0;
        }

        public bool Update(BaseUser entity)
        {
            _unitOfWork.UserRepository.Update(entity);
            return _unitOfWork.SaveChange() > 0;
        }

        public async Task<int> GetCountAsync()
        {
            return await _unitOfWork.UserRepository.GetCountAsync();
        }

        public  async Task<Pagination<BaseUser>> GetFilterAsync(UserFilteringModel entity, int pageIndex, int pageSize)
        {
            var baseUsers = _unitOfWork.UserRepository.GetFilter(entity);
            var pagination = _unitOfWork.UserRepository.ToPagination(baseUsers, pageIndex, pageSize);
            return pagination;
        }

        public async Task<UserLoginDTOResponse> LoginAsync(UserLoginDTO userObject)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAndPasswordHash(userObject.Email, userObject.Password);


            var refreshToken = RefreshTokenString.GetRefreshToken();
            var accessToken = user.GenerateJsonWebToken(_configuration.JWTSecretKey, _currentTime.GetCurrentTime());
            var expireRefreshTokenTime = DateTime.Now.AddHours(24);

            user.RefreshToken = refreshToken;
            user.ExpireTokenTime = expireRefreshTokenTime;
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return new UserLoginDTOResponse
            {
                UserId = user.Id,
                JWT =accessToken ,
                RefreshToken = refreshToken, 
            };
        }

        public async Task<UserToken> RefreshToken(string accessToken, string refreshToken)
        {
            if (accessToken.IsNullOrEmpty() || refreshToken.IsNullOrEmpty())
            {
                return null;
            }
            var principal = accessToken.GetPrincipalFromExpiredToken(_configuration.JWTSecretKey);

            var id = principal?.FindFirstValue("userID");
            _ = Guid.TryParse(id, out Guid userID);
            var userLogin = await _unitOfWork.UserRepository.GetByIdAsync(userID);
            if (userLogin == null || userLogin.RefreshToken != refreshToken || userLogin.ExpireTokenTime <= DateTime.Now)
            {
                return null;
            }

            var newAccessToken = userLogin.GenerateJsonWebToken(_configuration.JWTSecretKey, _currentTime.GetCurrentTime());
            var newRefreshToken = RefreshTokenString.GetRefreshToken();

            userLogin.RefreshToken = newRefreshToken;
            userLogin.ExpireTokenTime = DateTime.Now.AddDays(1);
            _unitOfWork.UserRepository.Update(userLogin);
            await _unitOfWork.SaveChangesAsync();

            return new UserToken
            {
                Email = userLogin.Email,
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

    }
}
