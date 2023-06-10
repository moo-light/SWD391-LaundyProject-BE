﻿using Application.ViewModels;
using Application.ViewModels.UserViewModels;
using Domain.Entities;

namespace Application.Interfaces.Services;

public interface IDriverService
{
    public Task RegisterAsync(UserRegisterDTO userObject);
    public Task<UserLoginDTOResponse> LoginAsync(UserLoginDTO userObject);
    Task<bool> AddAsync(Driver user);
    bool Remove(Guid entityId);
    bool Update(Driver entity);
    Task<Driver?> GetByIdAsync(Guid entityId);
    Task<IEnumerable<Driver>> GetAllAsync();
    Task<int> GetCountAsync();
    Task<IEnumerable<Driver>> GetFilterAsync(UserFilteringModel entity);
}
