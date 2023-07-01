using Application.Commons;
using Application.ViewModels.FilterModels;
using Application.ViewModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels.Batchs;
using Application.ViewModels.Buildings;

namespace Application.Interfaces.Services
{
    public interface IBatchService
    {
        Task<bool> AddAsync(BatchRequestDTO batch);
        Task<IEnumerable<BatchResponseDTO>> GetAllAsync();
        Task<Batch?> GetByIdAsync(Guid entityId);
        Task<int> GetCountAsync();
        Task<IEnumerable<Batch>> GetFilterAsync(BatchFilteringModel driver);
        bool Remove(Guid entityId);
        Task<bool> Update(Guid id, BatchRequestDTO entity);
        Task<Pagination<BatchResponseDTO>> GetBatchListPagi(int pageIndex, int pageSize);
        Task<Pagination<BatchResponseDTO>> GetFilterAsync(BatchFilteringModel batch, int pageIndex, int pageSize);
    }
}
