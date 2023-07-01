using Application.Commons;
using Application.ViewModels.FilterModels;
using Application.ViewModels;
using Application.ViewModels.OrderInBatch;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IOrderInBatchService
    {
        Task<OrderInBatch?> GetByIdAsync(Guid entityId);
        Task<int> GetCountAsync();
        Task<Pagination<OrderInBatch>> GetCustomerListPagi(int pageIndex, int pageSize);
        Task<Pagination<OrderInBatchResponseDTO>> GetAllAsync(int pageIndex, int pageSize);
        Task<Pagination<OrderInBatchResponseDTO>> GetFilterAsync(OrderInBatchFilteringModel entity, int pageIndex, int pageSize);
        Task<bool> UpdateAsync(Guid id, OrderInBatchRequestDTO orderInBatchRequest);
        Task<bool> RemoveAsync(Guid entityId);
        Task<bool> AddAsync(OrderInBatchRequestDTO orderInBatchRequest);
    }
}
