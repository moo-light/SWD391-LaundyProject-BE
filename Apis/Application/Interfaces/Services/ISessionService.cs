using Application.Commons;
using Application.ViewModels.BatchOfBuildings;
using Application.ViewModels.FilterModels;
using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface ISessionService
    {
        Task<bool> AddAsync(BatchOfBuildingRequestDTO timeSlot);
        Task<Pagination<BatchOfBuildingResponseDTO>> GetAllAsync(int pageIndex , int pageSize );
        Task<BatchOfBuilding?> GetByIdAsync(Guid entityId);
        Task<int> GetCount();
        Task<Pagination<BatchOfBuildingResponseDTO>> GetFilterAsync(SessionFilteringModel entity, int pageIndex, int pageSize);
        Task<Pagination<BatchOfBuilding>> GetCustomerListPagi(int pageIndex, int pageSize);
        Task<bool> UpdateAsync(Guid id, BatchOfBuildingRequestDTO sessionRequest);
        Task<bool> RemoveAsync(Guid entityId);
    }
}