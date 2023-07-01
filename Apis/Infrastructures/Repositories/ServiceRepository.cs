using Application.ViewModels.FilterModels;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Utils;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

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
            var seed = Includes(_dbSet.AsNoTracking(), x => x.OrderDetails, x => x.Store);
            result = predicates.Aggregate(seed.AsEnumerable(), (a, b) => a.Where(b.Compile()));

            return result;
        }
    }
}
