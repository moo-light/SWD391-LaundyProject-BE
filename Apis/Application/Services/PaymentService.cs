using Application.Commons;
using Application.ViewModels.FilterModels;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.ViewModels;
using Application.ViewModels.Payments;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PaymentService : IPaymentService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(PaymentRequestDTO paymentRequest)
        {
            Payment newPayment = _mapper.Map<Payment>(paymentRequest);
            await _unitOfWork.PaymentRepository.AddAsync(newPayment);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }


        public async Task<Payment?> GetByIdAsync(Guid entityId)
        {
            Payment? payment = await _unitOfWork.PaymentRepository.GetByIdAsync(entityId,x=>x.Order);
            return payment;
        }

        public async Task<int> GetCountAsync()
        {
            return await _unitOfWork.PaymentRepository.GetCountAsync();
        }

        public Task<Pagination<Payment>> GetCustomerListPagi(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<Pagination<PaymentResponseDTO>> GetAllAsync(int pageIndex, int pageSize)
        {
            var payments = await _unitOfWork.PaymentRepository.ToPagination(pageIndex, pageSize,x=>x.Order);
            return _mapper.Map<Pagination<PaymentResponseDTO>>(payments);
        }

        public async Task<Pagination<PaymentResponseDTO>> GetFilterAsync(PaymentFilteringModel entity, int pageIndex, int pageSize)
        {
            IEnumerable<Payment> payments = _unitOfWork.PaymentRepository.GetFilter(entity);
            var pagination = _unitOfWork.PaymentRepository.ToPagination(payments, pageIndex, pageSize);
            return _mapper.Map<Pagination<PaymentResponseDTO>>(pagination);
        }

        public async Task<bool> RemoveAsync(Guid entityId)
        {
            var result = _unitOfWork.PaymentRepository.SoftRemoveByID(entityId);
            if (result == false) return false;
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Guid id, PaymentRequestDTO paymentRequest)
        {
            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(id);
           payment= _mapper.Map(paymentRequest, payment);
            _unitOfWork.PaymentRepository.Update(payment);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

    }
}
