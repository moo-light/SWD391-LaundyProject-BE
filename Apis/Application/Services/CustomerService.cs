using Application.Commons;
using Application.ViewModels.FilterModels;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Utils;
using Application.ViewModels.Customer;
using Application.ViewModels.UserViewModels;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.Eventing.Reader;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentTime _currentTime;
        private readonly AppConfiguration _configuration;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentTime currentTime, AppConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentTime = currentTime;
            _configuration = configuration;
        }
        public async Task<IEnumerable<CustomerResponseDTO>> GetAllAsync()
        {
            List<Customer> customers = await _unitOfWork.CustomerRepository.GetAllAsync(x => x.Feedbacks, x => x.Orders);
            return _mapper.Map<List<CustomerResponseDTO>>(customers);
        }

        public async Task<Customer?> GetByIdAsync(Guid entityId)
        {
            Customer? customer = await _unitOfWork.CustomerRepository.GetByIdAsync(entityId, x=>x.Feedbacks,x=>x.Orders);
            return customer;
        }

        public async Task<bool> AddAsync(CustomerRequestDTO customer)
        {
            var newCustomer = _mapper.Map<Customer>(customer);
            if (newCustomer == null) return false;
            if (await _unitOfWork.UserRepository.CheckEmailExisted(newCustomer.Email)) throw new InvalidDataException("Email Exist!");
            await _unitOfWork.CustomerRepository.AddAsync(newCustomer);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveAsync(Guid entityId)
        {
            _unitOfWork.CustomerRepository.SoftRemoveByID(entityId);
            return _unitOfWork.SaveChange() > 0;
        }

        public async Task<bool> UpdateAsync(Guid id, CustomerRequestUpdateDTO entity)
        {
            

            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);

            if (customer.Email != entity.Email)
            {
                if (await _unitOfWork.UserRepository.CheckEmailExisted(entity.Email)) throw new InvalidDataException("Email Exist!");
            }

            if (entity.FullName == null) entity.FullName = customer.FullName;
            if (entity.Email == null) entity.Email = customer.Email;
            if (entity.Address == null) entity.Address = customer.Address;
            if (entity.PhoneNumber == null) entity.PhoneNumber = customer.PhoneNumber;

            customer = _mapper.Map(entity, customer);
            _unitOfWork.CustomerRepository.Update(customer);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<UserLoginDTOResponse> LoginAsync(UserLoginDTO userObject)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAndPasswordHash(userObject.Email, userObject.Password);
            return new UserLoginDTOResponse
            {
                UserId = user.Id,
                JWT = user.GenerateJsonWebToken(_configuration.JWTSecretKey, _currentTime.GetCurrentTime())
            };
        }
        public async Task<bool> CheckEmail(CustomerRegisterDTO userObject)
        {
            var isExited = await _unitOfWork.UserRepository.CheckEmailExisted(userObject.Email);
            if (isExited)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> RegisterAsync(CustomerRegisterDTO customer)
        {

            var newCustomer = _mapper.Map<Customer>(customer);

            await _unitOfWork.UserRepository.AddAsync(newCustomer);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<int> GetCountAsync()
        {
            return await _unitOfWork.CustomerRepository.GetCountAsync();
        }

        public async Task<Pagination<CustomerResponseDTO>> GetFilterAsync(CustomerFilteringModel customer, int pageIndex, int pageSize)
        {
            var query = _unitOfWork.CustomerRepository.GetFilter(customer);
            var customers = query.Where(c => c.IsDeleted == false).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var pagination = new Pagination<Customer>()
            {
                TotalItemsCount = query.Where(c => c.IsDeleted == false).Count(),
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = customers,
            };
            return _mapper.Map<Pagination<CustomerResponseDTO>>(pagination);
        }

        public UserLoginDTOResponse LoginAdmin(UserLoginDTO loginObject)
        {
            if (loginObject.Email.CheckPassword(_configuration.AdminAccount.Email))
                if (loginObject.Password.CheckPassword(_configuration.AdminAccount.Password))
                {
                    var refreshToken = GenerateRefreshToken();

                    return new UserLoginDTOResponse
                    {
                        UserId = Guid.NewGuid(),
                        JWT = new BaseUser()
                        {
                            IsAdmin = true,
                            Email = _configuration.AdminAccount.Email,
                            Id = Guid.NewGuid()//admin want to be anonymous
                        }.GenerateJsonWebToken(_configuration.JWTSecretKey, _currentTime.GetCurrentTime()),
                        RefreshToken = refreshToken
                    };
                }
            throw new EventLogInvalidDataException("Warning after 5 more tries this page will be disabled");
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }

        public async Task<Pagination<CustomerResponseDTO>> GetCustomerListPagi(int pageIndex = 0, int pageSize = 10)
        {
            var customers = await _unitOfWork.CustomerRepository.ToPagination(pageIndex, pageSize);
            return _mapper.Map<Pagination<CustomerResponseDTO>>(customers);
        }

        public AdminToken RefreshToken(string refreshToken)
        {
            return new AdminToken
            {
                Id = Guid.NewGuid(),
                JwtToken = new BaseUser()
                {
                    IsAdmin = true,
                    Email = _configuration.AdminAccount.Email,
                    Id = Guid.NewGuid()
                }.GenerateJsonWebToken(_configuration.JWTSecretKey, _currentTime.GetCurrentTime()),
                RefreshToken = GenerateRefreshToken()
            };
        }
    }
}
