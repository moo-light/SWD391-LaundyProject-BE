using Application.Commons;
using Application.ViewModels.FilterModels;
using Application.ViewModels;
using Application.ViewModels.Payments;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<Payment?> GetByIdAsync(Guid entityId);
        Task<int> GetCountAsync();
        Task<Pagination<Payment>> GetCustomerListPagi(int pageIndex, int pageSize);
        Task<Pagination<PaymentResponseDTO>> GetAllAsync(int pageIndex, int pageSize);
        Task<Pagination<PaymentResponseDTO>> GetFilterAsync(PaymentFilteringModel entity, int pageIndex, int pageSize);
        Task<bool> RemoveAsync(Guid entityId);
        Task<bool> UpdateAsync(Guid id, PaymentRequestDTO paymentRequest);
        Task<bool> AddAsync(PaymentRequestDTO paymentRequest);
    }
}
