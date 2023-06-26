﻿using Application.Commons;
using Application.ViewModels.FilterModels;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BatchService : IBatchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsService _claimsService;
        private readonly IMapper _mapper;

        public BatchService(IUnitOfWork unitOfWork, IClaimsService claimsService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _claimsService = claimsService;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(Batch batchDTO)
        {
            if (_claimsService.GetCurrentUserId == Guid.Empty) throw new AuthenticationException("User not login");
            var batchId = Guid.NewGuid();
            Guid driverId = _claimsService.GetCurrentUserId;
            var batch = _mapper.Map<Batch>(batchDTO);
            batch.Id = batchId;
            batch.DriverId = driverId;

            await _unitOfWork.BatchRepository.AddAsync(batch);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<Batch>> GetAllAsync() => await _unitOfWork.BatchRepository.GetAllAsync();

        public async Task<Batch?> GetByIdAsync(Guid entityId) => await _unitOfWork.BatchRepository.GetByIdAsync(entityId);

        public async Task<int> GetCountAsync() => await _unitOfWork.BatchRepository.GetCountAsync();

        public async Task<Pagination<Batch>> GetBatchListPagi(int pageIndex, int pageSize)
        {
            var batchs = await _unitOfWork.BatchRepository.ToPagination(pageIndex, pageSize);
            var batchListPagination = new Pagination<Batch>
            {
                PageIndex = batchs.PageIndex,
                PageSize = batchs.PageSize,
                TotalItemsCount = batchs.TotalItemsCount,
                Items = batchs.Items.Select(b => new Batch
                {
                    Id = b.Id,
                    Type = b.Type,
                    Status = b.Status,
                    Driver = b.Driver,
                }).ToList(),
            };
            return batchListPagination;
        }

        public async Task<IEnumerable<Batch>> GetFilterAsync(BatchFilteringModel entity)
        {
            var o = _unitOfWork.BatchRepository.GetFilter(entity).ToList();
            return _mapper.Map<IEnumerable<Batch>>(o);
        }

        public bool Remove(Guid entityId)
        {
           _unitOfWork.BatchRepository.SoftRemoveByID(entityId);
            return _unitOfWork.SaveChange() > 0; 
        }

        public bool Update(Batch entity)
        {
            _unitOfWork.BatchRepository.Update(entity);
            return _unitOfWork.SaveChange() > 0;
        }
        Task<IEnumerable<Batch>> IBatchService.GetBatchListPagi(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
