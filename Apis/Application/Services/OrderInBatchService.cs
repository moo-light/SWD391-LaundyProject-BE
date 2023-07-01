using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.ViewModels.FilterModels;
using Application.ViewModels.OrderInBatch;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class OrderInBatchService : IOrderInBatchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderInBatchService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> AddAsync(OrderInBatchRequestDTO orderInBatchRequest)
        {
            OrderInBatch newOrderInBatch = _mapper.Map<OrderInBatch>(orderInBatchRequest);
            await _unitOfWork.OrderInBatchRepository.AddAsync(newOrderInBatch);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<OrderInBatch?> GetByIdAsync(Guid entityId)
        {
            return await _unitOfWork.OrderInBatchRepository.GetByIdAsync(entityId,x=>x.Order,x=>x.Batch);
        }

        public async Task<int> GetCountAsync() => await _unitOfWork.OrderInBatchRepository.GetCountAsync();

        public Task<Pagination<OrderInBatch>> GetCustomerListPagi(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<Pagination<OrderInBatchResponseDTO>> GetAllAsync(int pageIndex, int pageSize)
        {
            var ordersInBatch = await _unitOfWork.OrderInBatchRepository.ToPagination(pageIndex, pageSize, x => x.Order, x => x.Batch);
            return _mapper.Map<Pagination<OrderInBatchResponseDTO>>(ordersInBatch);
        }

        public async Task<Pagination<OrderInBatchResponseDTO>> GetFilterAsync(OrderInBatchFilteringModel entity, int pageIndex, int pageSize)
        {
            IEnumerable<OrderInBatch> ordersInBatch = _unitOfWork.OrderInBatchRepository.GetFilter(entity);
            var pagination = _unitOfWork.OrderInBatchRepository.ToPagination(ordersInBatch, pageIndex, pageSize);
            return _mapper.Map<Pagination<OrderInBatchResponseDTO>>(pagination);
        }

        public async Task<bool> RemoveAsync(Guid entityId)
        {
            var result = _unitOfWork.OrderInBatchRepository.SoftRemoveByID(entityId);
            if (result == false) return false;
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Guid id, OrderInBatchRequestDTO orderInBatchRequest)
        {
            var orderInBatch = await _unitOfWork.OrderInBatchRepository.GetByIdAsync(id);
           orderInBatch= _mapper.Map(orderInBatchRequest, orderInBatch);
            _unitOfWork.OrderInBatchRepository.Update(orderInBatch);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

    }
}
