using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.ViewModels.FilterModels;
using Application.ViewModels.BatchOfBuildings;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Pagination<BatchOfBuildingResponseDTO>> GetAllAsync(int pageIndex, int pageSize)
        {
            var stores = await _unitOfWork.StoreRepository.ToPagination(pageIndex, pageSize,x=>x.Feedbacks,x=>x.Orders,x=>x.Services);
            return _mapper.Map<Pagination<BatchOfBuildingResponseDTO>>(stores);
        }

        public async Task<BatchOfBuilding?> GetByIdAsync(Guid entityId) => await _unitOfWork.SessionRepository.GetByIdAsync(entityId);
        public async Task<bool> AddAsync(BatchOfBuildingRequestDTO timeSlot)
        {
            BatchOfBuilding newTimeSlot = _mapper.Map<BatchOfBuilding>(timeSlot);
            await _unitOfWork.SessionRepository.AddAsync(newTimeSlot);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveAsync(Guid entityId)
        {
            var result =  _unitOfWork.SessionRepository.SoftRemoveByID(entityId);
            if (result == false) return false;
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Guid id, BatchOfBuildingRequestDTO sessionRequest)
        {
            var session = await _unitOfWork.SessionRepository.GetByIdAsync(id);
            session= _mapper.Map(sessionRequest, session);
            _unitOfWork.SessionRepository.Update(session);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<Pagination<BatchOfBuildingResponseDTO>> GetFilterAsync(SessionFilteringModel entity, int pageIndex, int pageSize)
        {
            IEnumerable<BatchOfBuilding> sessions = _unitOfWork.SessionRepository.GetFilter(entity);
            var pagination = _unitOfWork.SessionRepository.ToPagination(sessions, pageIndex, pageSize);
            return _mapper.Map<Pagination<BatchOfBuildingResponseDTO>>(pagination);
        }

        public async Task<int> GetCount()
        {
            return await _unitOfWork.SessionRepository.GetCountAsync();
        }

        public Task<Pagination<BatchOfBuilding>> GetCustomerListPagi(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
