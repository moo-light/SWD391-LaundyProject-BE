using Application.ViewModels.FilterModels;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Utils;
using Application.ViewModels;
using Domain.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

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
            var seed = Includes(_dbSet.AsNoTracking(), x => x.Feedbacks, x => x.Services,x=>x.Orders);

            var result = predicates.Aggregate(_dbSet.AsEnumerable(), (a, b) => a.Where(b.Compile()));

            return result;
        }
    }
}
