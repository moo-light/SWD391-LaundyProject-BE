using Application.Commons;
using Application.ViewModels.FilterModels;
using Application.ViewModels;
using Application.ViewModels.LaundryOrders;
using Domain.Entities;

namespace Application.Interfaces.Services;

public interface IOrderService
{
    Task<LaundryOrder?> GetByIdAsync(Guid entityId);
    Task<int> GetCountAsync();
    Task<Pagination<LaundryOrder>> GetCustomerListPagi(int pageIndex, int pageSize);
    Task<Pagination<LaundryOrderResponseDTO>> GetAllAsync(int pageIndex, int pageSize);
    Task<Pagination<LaundryOrderResponseDTO>> GetFilterAsync(LaundryOrderFilteringModel entity, int pageIndex, int pageSize);
    Task<bool> RemoveAsync(Guid entityId);
    Task<bool> UpdateAsync(Guid id, LaundryOrderRequestDTO orderRequest);
    Task<bool> AddAsync(LaundryOrderRequestAddDTO orderRequest);
}