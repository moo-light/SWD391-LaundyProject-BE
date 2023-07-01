using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.ViewModels.FilterModels;
using Application.ViewModels.Stores;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Pagination<ServiceResponseDTO>> GetAllAsync(int pageIndex, int pageSize)
        {
            var services = await _unitOfWork.ServiceRepository.ToPagination(pageIndex, pageSize, x=>x.Store,x=>x.OrderDetails);
            return _mapper.Map<Pagination<ServiceResponseDTO>>(services);
        }
        public async Task<Service?> GetByIdAsync(Guid entityId)
        {
            Service? service = await _unitOfWork.ServiceRepository.GetByIdAsync(entityId,x=>x.Store,x=>x.OrderDetails);
            return service;
        }
        public async Task<bool> AddAsync(ServiceRequestDTO serviceRequest)
        {
            Service newService = _mapper.Map<Service>(serviceRequest);
            await _unitOfWork.ServiceRepository.AddAsync(newService);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveAsync(Guid entityId)
        {
            var result = _unitOfWork.ServiceRepository.SoftRemoveByID(entityId);
            if (result == false) return false;
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(Guid id, ServiceRequestDTO serviceRequest)
        {
            var service = await _unitOfWork.ServiceRepository.GetByIdAsync(id);
            service = _mapper.Map(serviceRequest, service);
            _unitOfWork.ServiceRepository.Update(service);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }


        public async Task<int> GetCountAsync()
        {
            return await _unitOfWork.ServiceRepository.GetCountAsync();
        }

        public async Task<Pagination<ServiceResponseDTO>> GetFilterAsync(ServiceFilteringModel entity, int pageIndex, int pageSize)
        {
            IEnumerable<Service> services = _unitOfWork.ServiceRepository.GetFilter(entity);
            var pagination = _unitOfWork.ServiceRepository.ToPagination(services, pageIndex, pageSize);
            return _mapper.Map<Pagination<ServiceResponseDTO>>(pagination);
        }

        public Task<Pagination<Service>> GetCustomerListPagi(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
