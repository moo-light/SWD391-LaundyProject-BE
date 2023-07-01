using Application.Commons;
using Application.ViewModels.FilterModels;
using Application.ViewModels.Stores;
using Domain.Entities;
using System.Formats.Tar;

namespace Application.Interfaces.Services
{
    public interface IStoreService
    {
        Task<bool> AddAsync(StoreRequestDTO store);
        Task<Pagination<StoreResponseDTO>> GetAllAsync(int pageIndex,int PageSize);
        Task<Store?> GetByIdAsync(Guid entityId);
        Task<int> GetCountAsync();
        Task<Pagination<StoreResponseDTO>> GetFilterAsync(StoreFilteringModel entity, int pageIndex, int pageSize);
        Task<Pagination<Store>> GetCustomerListPagi(int pageIndex, int pageSize);
        Task<bool> RemoveAsync(Guid entityId);
        Task<bool> UpdateAsync(Guid id, StoreRequestDTO entity);
    }
}