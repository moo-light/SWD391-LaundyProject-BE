﻿using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Utils;
using Application.ViewModels;
using Application.ViewModels.FilterModels;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Infrastructures.Repositories
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        private readonly AppDbContext _dbContext;

    public ServiceRepository(AppDbContext dbContext,
        ICurrentTime timeService,
        IClaimsService claimsService)
        : base(dbContext,
              timeService,
              claimsService)
    {
        _dbContext = dbContext;
    }

        public IEnumerable<Service> GetFilter(ServiceFilteringModel entity)
        {
            entity ??= new();
            IEnumerable<Service> result;
            Expression<Func<Service, bool>> storeId = x => entity.StoreId.EmptyOrEquals(x.StoreId);
            Expression<Func<Service, bool>> paymentMethod = x => entity.PricePerKg.EmptyOrContainedIn(x.PricePerKg.ToString());
            Expression<Func<Service, bool>> date = x => x.CreationDate.IsInDateTime(entity);

            var predicates = ExpressionUtils.CreateListOfExpression(storeId, paymentMethod,  date);

            result = predicates.Aggregate(_dbSet.AsEnumerable(), (a, b) => a.Where(b.Compile()));

            return result;
        }
    }
}
