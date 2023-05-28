﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IPackageService
    {
        Task<bool> AddAsync(Package package);
        Task<IEnumerable<Package>> GetAllAsync();
        Task<Package?> GetByIdAsync(Guid entityId);
        bool Remove(Guid entityId);
        bool Update(Package entity);
    }
}