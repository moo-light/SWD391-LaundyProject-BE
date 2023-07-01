using Application;
using Application.Commons;
using Application.ViewModels.FilterModels;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Utils;
using Application.ViewModels;
using Application.ViewModels.UserViewModels;
using AutoMapper;
using Domain.Entities;
using Application.ViewModels.Drivers;
using Microsoft.Extensions.Configuration;
using Application.ViewModels.Drivers;
using Application.ViewModels.Customer;

namespace Application.Services
{
    public class DriverService : IDriverService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentTime _currentTime;
        private readonly AppConfiguration _configuration;

        public DriverService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentTime currentTime, AppConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentTime = currentTime;
            _configuration = configuration;
        }

        public async Task<IEnumerable<DriverResponseDTO>> GetAllAsync()
        {
            List<Driver> drivers = await _unitOfWork.DriverRepository.GetAllAsync(x=>x.Batches);
            return _mapper.Map<List<DriverResponseDTO>>(drivers);
        }

        public async Task<Driver?> GetByIdAsync(Guid entityId)
        {
            return await _unitOfWork.DriverRepository.GetByIdAsync(entityId,x=>x.Batches);
        }

        public async Task<bool> AddAsync(DriverRequestDTO driver)
        {
            var newDriver = _mapper.Map<Driver>(driver);
            if (newDriver == null) return false;
            await _unitOfWork.DriverRepository.AddAsync(newDriver);
            return await _unitOfWork.SaveChangesAsync() >0;
        }

        public async Task<bool> RemoveAsync(Guid entityId)
        {
             _unitOfWork.DriverRepository.SoftRemoveByID(entityId);
            return _unitOfWork.SaveChange() > 0;
        }

        public async Task<bool> Update(Guid id, DriverRequestUpdateDTO entity)
        {
            var driver = await _unitOfWork.DriverRepository.GetByIdAsync(id);
            if (driver.Email != entity.Email)
            {
                if (await _unitOfWork.UserRepository.CheckEmailExisted(entity.Email)) throw new InvalidDataException("Email Exist!");
            }

            if (entity.FullName == null) entity.FullName = driver.FullName;
            if (entity.Email == null) entity.Email = driver.Email;
            if (entity.PhoneNumber == null) entity.PhoneNumber = driver.PhoneNumber;

            driver = _mapper.Map(entity, driver);
            _unitOfWork.DriverRepository.Update(driver);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<UserLoginDTOResponse> LoginAsync(UserLoginDTO userObject)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAndPasswordHash(userObject.Email, userObject.Password.Hash());
            return new UserLoginDTOResponse
            {
                UserId = user.Id,
                JWT = user.GenerateJsonWebToken(_configuration.JWTSecretKey, _currentTime.GetCurrentTime())
            };
    }
        public async Task<bool> CheckEmail(ViewModels.Drivers.DriverRegisterDTO driverRegisterDTO)
        {
            var isExited = await _unitOfWork.UserRepository.CheckEmailExisted(driverRegisterDTO.Email);
            if (isExited)
            {
                return true;
            }
            else return false;
        }

        public async Task<bool> RegisterAsync(ViewModels.Drivers.DriverRegisterDTO driver)
        {
            var newDriver = _mapper.Map<Driver>(driver);

            await _unitOfWork.DriverRepository.AddAsync(newDriver);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<int> GetCountAsync()
        {
            return await _unitOfWork.DriverRepository.GetCountAsync();
        }

        public async Task<IEnumerable<Driver>> GetFilterAsync(DriverFilteringModel driver)
        {
            return _unitOfWork.DriverRepository.GetFilter(driver).ToList();
        }

        public Task<Pagination<Driver>> GetCustomerListPagi(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<Pagination<DriverResponseDTO>> GetFilterAsync(DriverFilteringModel customer, int pageIndex, int pageSize)
        {
            var query = _unitOfWork.DriverRepository.GetFilter(customer);
            var customers = query.Where(c => c.IsDeleted == false).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var pagination = new Pagination<Driver>()
            {
                TotalItemsCount = query.Where(c => c.IsDeleted == false).Count(),
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = customers,
            };
            return _mapper.Map<Pagination<DriverResponseDTO>>(pagination);
        }
    }
}
