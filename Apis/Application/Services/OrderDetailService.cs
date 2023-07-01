using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.ViewModels;
using Application.ViewModels.FilterModels;
using Application.ViewModels.OrderDetails;
using Application.ViewModels.OrderInBatch;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderDetailService : IOrderDetail
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderDetailService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> AddAsync(OrderDetailRequestDTO orderDetailRequest)
        {
            OrderDetail newOrderDetail = _mapper.Map<OrderDetail>(orderDetailRequest);
            await _unitOfWork.OrderDetailRepository.AddAsync(newOrderDetail);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<OrderDetail?> GetByIdAsync(Guid entityId)
        {
            return await _unitOfWork.OrderDetailRepository.GetByIdAsync(entityId,x=>x.Order,x=>x.Service);
        }

        public async Task<int> GetCountAsync()
        {
           return await _unitOfWork.OrderDetailRepository.GetCountAsync();
        }

        public Task<Pagination<OrderDetail>> GetCustomerListPagi(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<Pagination<OrderDetailResponseDTO>> GetAllAsync(int pageIndex, int pageSize)
        {
            var ordersInBatch = await _unitOfWork.OrderDetailRepository.ToPagination(pageIndex, pageSize,x=>x.Order,x=>x.Service);
            return _mapper.Map<Pagination<OrderDetailResponseDTO>>(ordersInBatch);
        }

        public async Task<Pagination<OrderDetailResponseDTO>> GetFilterAsync(OrderDetailFilteringModel entity, int pageIndex, int pageSize)
        {
            var orderDetail = _unitOfWork.OrderDetailRepository.GetFilter(entity);
            var pagination = _unitOfWork.OrderDetailRepository.ToPagination(orderDetail, pageIndex, pageSize);
            return _mapper.Map<Pagination<OrderDetailResponseDTO>>(pagination);
        }
        public async Task<bool> RemoveAsync(Guid entityId)
        {
            var result = _unitOfWork.OrderDetailRepository.SoftRemoveByID(entityId);
            if (result == false) return false;
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(Guid id, OrderDetailRequestDTO orderDetailRequest)
        {
            var orderDetail = await _unitOfWork.OrderDetailRepository.GetByIdAsync(id);
            orderDetail = _mapper.Map(orderDetailRequest, orderDetail);
            _unitOfWork.OrderDetailRepository.Update(orderDetail);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

    }
}
