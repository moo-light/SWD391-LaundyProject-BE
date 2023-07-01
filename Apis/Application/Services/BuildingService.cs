using Application.Commons;
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
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels.Buildings;
using Application.ViewModels.Customer;
using Application.ViewModels.Batchs;

namespace Application.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentTime _currentTime;
        public BuildingService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentTime currentTime)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentTime = currentTime; 
        }

        public async Task<bool> AddAsync(BuildingRequestDTO building)
        {
            var newBuilding = _mapper.Map<Building>(building);
            if (newBuilding == null) return false;
            await _unitOfWork.BuildingRepository.AddAsync(newBuilding);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<BuildingResponseDTO>> GetAllAsync()
        {
            var o = _unitOfWork.BuildingRepository.GetAllAsync(x=>x.Orders,x=>x.BatchOfBuildings).ToString();
            return _mapper.Map<List<BuildingResponseDTO>>(o);
        }

        public async Task<Building?> GetByIdAsync(Guid entityId)
        {
            return await _unitOfWork.BuildingRepository.GetByIdAsync(entityId,x=>x.BatchOfBuildings,x=>x.Orders);
        }

        public async Task<int> GetCountAsync()
        {
            return await _unitOfWork.BuildingRepository.GetCountAsync();
        }

        public async Task<Pagination<BuildingResponseDTO>> GetCustomerListPagi(int pageIndex, int pageSize)
        {
            var building = await _unitOfWork.BuildingRepository.ToPagination(pageIndex, pageSize, x => x.Orders, x => x.BatchOfBuildings);
            return _mapper.Map<Pagination<BuildingResponseDTO>>(building);
        }

        public async Task<Pagination<Building>> GetFilterAsync(BuildingFilteringModel entity)
        {
            var o = _unitOfWork.BuildingRepository.GetFilter(entity).ToList();
            return _mapper.Map<Pagination<Building>>(o);   
        }

        public async Task<Pagination<BuildingResponseDTO>> GetFilterAsync(BuildingFilteringModel building, int pageIndex, int pageSize)
        {
            var query = _unitOfWork.BuildingRepository.GetFilter(building);
            var buildings = query.Where(c => c.IsDeleted == false).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var pagination = new Pagination<Building>()
            {
                TotalItemsCount = query.Where(c => c.IsDeleted == false).Count(),
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = buildings
            };
            return _mapper.Map<Pagination<BuildingResponseDTO>>(pagination);
        }

        public bool Remove(Guid entityId)
        {
            _unitOfWork.BuildingRepository.SoftRemoveByID(entityId);
            return _unitOfWork.SaveChange() > 0;
        }

        public async Task<bool> Update(Guid id, BuildingRequestDTO entity)
        {
            var customer = await _unitOfWork.BuildingRepository.GetByIdAsync(id);
            //if (customer.Email != entity.Email)
            //{
            //    if (await _unitOfWork.UserRepository.CheckEmailExisted(entity.Email)) throw new InvalidDataException("Email Exist!");
            //}

            //if (entity.FullName == null) entity.FullName = customer.FullName;
            //if (entity.Email == null) entity.Email = customer.Email;
            //if (entity.Address == null) entity.Address = customer.Address;
            //if (entity.PhoneNumber == null) entity.PhoneNumber = customer.PhoneNumber;

            customer = _mapper.Map(entity, customer);
            _unitOfWork.BuildingRepository.Update(customer);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
