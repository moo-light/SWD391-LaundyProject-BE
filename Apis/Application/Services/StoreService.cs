using Application.Commons;
using Application.ViewModels.FilterModels;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.ViewModels;
using Application.ViewModels.Stores;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StoreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Pagination<StoreResponseDTO>> GetAllAsync(int pageindex, int pageSize)
        {
            var stores =  await _unitOfWork.StoreRepository.ToPagination(pageindex,pageSize,x=>x.Feedbacks,x=>x.Services,x=>x.Orders);
            return _mapper.Map<Pagination<StoreResponseDTO>>(stores);
        }

        public async Task<Store?> GetByIdAsync(Guid entityId)
        {
            Store? store = await _unitOfWork.StoreRepository.GetByIdAsync(entityId,x=>x.Services,x=>x.Orders,x=>x.Feedbacks);
            return store;
        }
        public async Task<bool> AddAsync(StoreRequestDTO store)
        {
            var newStore = _mapper.Map<Store>(store);
            await _unitOfWork.StoreRepository.AddAsync(newStore);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveAsync(Guid entityId)
        {
            var result = _unitOfWork.StoreRepository.SoftRemoveByID(entityId);
            if (result == false) return false;
            return await _unitOfWork.SaveChangesAsync() > 0;
        }


        public async Task<bool> UpdateAsync(Guid id, StoreRequestDTO entity)
        {
            var store = await _unitOfWork.StoreRepository.GetByIdAsync(id);
            if (store == null) return false;
            Store? newStore = _mapper.Map(entity, store);
            _unitOfWork.StoreRepository.Update(newStore);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<int> GetCountAsync()
        {
            return await _unitOfWork.StoreRepository.GetCountAsync();
        }

        public async Task<Pagination<StoreResponseDTO>> GetFilterAsync(StoreFilteringModel entity, int pageIndex, int pageSize)
        {
            IEnumerable<Store> stores = _unitOfWork.StoreRepository.GetFilter(entity);
            var pagination = _unitOfWork.StoreRepository.ToPagination(stores, pageIndex, pageSize);
            return _mapper.Map<Pagination<StoreResponseDTO>>(pagination);
        }

        public Task<Pagination<Store>> GetCustomerListPagi(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
