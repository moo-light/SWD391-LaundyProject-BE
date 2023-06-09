﻿using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Utils;
using Application.ViewModels;
using Application.ViewModels.FilterModels;
using Domain.Entities;
using System.Linq.Expressions;

namespace Infrastructures.Repositories
{
    public class StoreRepository : GenericRepository<Store>, IStoreRepository
    {
        private readonly AppDbContext _dbContext;

    public StoreRepository(AppDbContext dbContext,
        ICurrentTime timeService,
        IClaimsService claimsService)
        : base(dbContext,
              timeService,
              claimsService)
    {
        _dbContext = dbContext;
    }

        public  IEnumerable<Store> GetFilter(StoreFilteringModel entity)
        {
            entity ??= new();
            Expression<Func<Store, bool>> address = x => entity.Address.EmptyOrContainedIn(x.Address);
            Expression<Func<Store, bool>> name = x => entity.Name.EmptyOrContainedIn(x.Name);
            Expression<Func<Store, bool>> date = x => x.CreationDate.IsInDateTime(entity);

            var predicates = ExpressionUtils.CreateListOfExpression(address, name,date);

            var result = predicates.Aggregate(_dbSet.AsEnumerable(), (a, b) => a.Where(b.Compile()));

            return result;
        }
    }
}
