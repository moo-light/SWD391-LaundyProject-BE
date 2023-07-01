using Application.Commons;
using Application.ViewModels.FilterModels;
using Application.ViewModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels.Buildings;
using Application.ViewModels.Customer;

namespace Application.Interfaces.Services
{
    public interface IBuildingService
    {
        Task<bool> AddAsync(BuildingRequestDTO building);
        Task<IEnumerable<BuildingResponseDTO>> GetAllAsync();
        Task<Building?> GetByIdAsync(Guid entityId);
        Task<int> GetCountAsync();
        Task<Pagination<Building>> GetFilterAsync(BuildingFilteringModel entity);
        bool Remove(Guid entityId);
        Task<bool> Update(Guid id, BuildingRequestDTO entity);
        Task<Pagination<BuildingResponseDTO>> GetCustomerListPagi(int pageIndex, int pageSize);
        Task<Pagination<BuildingResponseDTO>> GetFilterAsync(BuildingFilteringModel customer, int pageIndex, int pageSize);
    }
}
