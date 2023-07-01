using Application.Commons;
using Application.ViewModels;
using Application.ViewModels.FilterModels;
using Application.ViewModels.OrderDetails;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IOrderDetail
    {
        Task<OrderDetail?> GetByIdAsync(Guid entityId);
        Task<int> GetCountAsync();
        Task<Pagination<OrderDetail>> GetCustomerListPagi(int pageIndex, int pageSize);
        Task<Pagination<OrderDetailResponseDTO>> GetFilterAsync(OrderDetailFilteringModel entity, int pageIndex, int pageSize);
        Task<Pagination<OrderDetailResponseDTO>> GetAllAsync(int pageIndex, int pageSize);
        Task<bool> AddAsync(OrderDetailRequestDTO orderDetailRequest);
        Task<bool> RemoveAsync(Guid entityId);
        Task<bool> UpdateAsync(Guid id, OrderDetailRequestDTO orderDetailRequest);
    }
}
