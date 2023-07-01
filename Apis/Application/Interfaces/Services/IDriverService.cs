using Application.Commons;
using Application.ViewModels.FilterModels;
using Application.ViewModels;
using Application.ViewModels.UserViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.ViewModels.Drivers;
using Application.ViewModels.Customer;

namespace Application.Interfaces.Services;

public interface IDriverService
{
    public Task<bool> RegisterAsync(ViewModels.Drivers.DriverRegisterDTO driver);
    public Task<UserLoginDTOResponse> LoginAsync(UserLoginDTO userObject);
    Task<bool> AddAsync(DriverRequestDTO user);
    Task<bool> RemoveAsync(Guid entityId);
    Task<bool> Update(Guid id, DriverRequestUpdateDTO entity);
    Task<Driver?> GetByIdAsync(Guid entityId);
    Task<IEnumerable<DriverResponseDTO>> GetAllAsync();
    Task<int> GetCountAsync();
    Task<bool> CheckEmail(ViewModels.Drivers.DriverRegisterDTO registerObject);
    Task<IEnumerable<Driver>> GetFilterAsync(DriverFilteringModel driver);
    Task<Pagination<Driver>> GetCustomerListPagi(int pageIndex, int pageSize);
    Task<Pagination<DriverResponseDTO>> GetFilterAsync(DriverFilteringModel customer, int pageIndex, int pageSize);

}
