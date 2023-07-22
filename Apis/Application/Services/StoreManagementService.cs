﻿using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.ViewModels.Drivers;
using Application.ViewModels.FilterModels;
using Application.ViewModels.StoreManagers;
using Application.ViewModels.Stores;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StoreManagementService : IStoreManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentTime _currentTime;
        private readonly AppConfiguration _configuration;
        public StoreManagementService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentTime currentTime, AppConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentTime = currentTime;
            _configuration = configuration;
        }

        public async Task<bool> AddAsync(StoreManagerRequestDTO user)
        {
            var newStoreManager = _mapper.Map<StoreManager>(user);
            if (newStoreManager == null) return false;
            await _unitOfWork.StoreManagerRepository.AddAsync(newStoreManager);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }


        public async Task<bool> CheckEmail(StoreManagerRegisterDTO driverRegisterDTO)
        {
            var isExited = await _unitOfWork.UserRepository.CheckEmailExisted(driverRegisterDTO.Email);
            if (isExited)
            {
                return true;
            }
            else return false;
        }

        public Task<IEnumerable<StoreManagerResponseDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Driver?> GetByIdAsync(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Pagination<StoreManagerResponseDTO>> GetFilterAsync(StoreFilteringModel customer, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterAsync(StoreManagerRegisterDTO storeUser)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Guid id, StoreManagerRequestUpdateDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}