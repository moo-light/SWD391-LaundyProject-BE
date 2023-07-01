using Application.Commons;
using Application.ViewModels.FilterModels;
using Application.ViewModels;
using Application.ViewModels.Stores;
using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface IServiceService
    {
        Task<Service?> GetByIdAsync(Guid entityId);
        Task<int> GetCountAsync();
        Task<Pagination<Service>> GetCustomerListPagi(int pageIndex, int pageSize);
        Task<Pagination<ServiceResponseDTO>> GetAllAsync(int pageIndex, int pageSize);
        Task<Pagination<ServiceResponseDTO>> GetFilterAsync(ServiceFilteringModel entity, int pageIndex, int pageSize);
        Task<bool> UpdateAsync(Guid id, ServiceRequestDTO serviceRequest);
        Task<bool> RemoveAsync(Guid entityId);
        Task<bool> AddAsync(ServiceRequestDTO serviceRequest);
    }
}