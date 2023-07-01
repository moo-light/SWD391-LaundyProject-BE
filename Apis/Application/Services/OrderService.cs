using Application.Commons;
using Application.ViewModels.FilterModels;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.ViewModels;
using Application.ViewModels.LaundryOrders;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimService;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
        }
        public async Task<LaundryOrder?> GetByIdAsync(Guid entityId)
        {
            LaundryOrder? laundryOrder = await _unitOfWork.OrderRepository.GetByIdAsync(entityId,
                                                                                        x => x.OrderInBatches,
                                                                                        x => x.OrderDetails,
                                                                                        x => x.Payments,
                                                                                        x => x.Customer,
                                                                                        x => x.Store,
                                                                                        x => x.Building);
            return laundryOrder;
        }
        public async Task<bool> AddAsync(LaundryOrderRequestAddDTO orderRequest)
        {
            LaundryOrder newOrder = _mapper.Map<LaundryOrder>(orderRequest);
            newOrder.CustomerId = _claimService.GetCurrentUserId;
             for(int i = 0; i < orderRequest.NumberOfPackages; i++)
            {
                newOrder.OrderDetails.Add(new OrderDetail
                {
                    Weight = default,
                    Status = nameof(OrderDetailStatus.Pending)
                });
            }
            await _unitOfWork.OrderRepository.AddAsync(newOrder);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveAsync(Guid entityId)
        {
            var result = _unitOfWork.OrderRepository.SoftRemoveByID(entityId);
            if (result == false) return false;
            return await _unitOfWork.SaveChangesAsync() > 0;
        }


        public async Task<bool> UpdateAsync(Guid id, LaundryOrderRequestDTO orderRequest)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            order= _mapper.Map(orderRequest, order);
            _unitOfWork.OrderRepository.Update(order);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }


        public async Task<int> GetCountAsync()
        {
            return await _unitOfWork.OrderRepository.GetCountAsync();
        }

        public async Task<Pagination<LaundryOrderResponseDTO>> GetAllAsync(int pageIndex, int pageSize)
        {
            var orders = await _unitOfWork.OrderRepository.ToPagination(pageIndex,
                                                                        pageSize,
                                                                        x => x.OrderInBatches,
                                                                        x => x.OrderDetails,
                                                                        x => x.Payments,
                                                                        x => x.Customer,
                                                                        x => x.Store,
                                                                        x => x.Building);
            return _mapper.Map<Pagination<LaundryOrderResponseDTO>>(orders);
        }

        public async Task<Pagination<LaundryOrderResponseDTO>> GetFilterAsync(LaundryOrderFilteringModel entity, int pageIndex, int pageSize)
        {
            IEnumerable<LaundryOrder> orders = _unitOfWork.OrderRepository.GetFilter(entity);
            var pagination = _unitOfWork.OrderRepository.ToPagination(orders, pageIndex, pageSize);
            return _mapper.Map<Pagination<LaundryOrderResponseDTO>>(pagination);
        }

        public Task<Pagination<LaundryOrder>> GetCustomerListPagi(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
