using Application.ViewModels.FilterModels;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Utils;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories
{
    public class OrderRepository : GenericRepository<LaundryOrder>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext,
            ICurrentTime timeService,
            IClaimsService claimsService)
            : base(dbContext,
                  timeService,
                  claimsService)
        {
        }
        public IEnumerable<LaundryOrder> GetFilter(LaundryOrderFilteringModel entity)
        {
            entity ??= new();
            Expression<Func<LaundryOrder, bool>> customerId = x => entity.CustomerId.IsNullOrEmpty() || entity.CustomerId.Any(y => x.CustomerId != null && x.CustomerId == y);
            Expression<Func<LaundryOrder, bool>> buildingId = x => entity.BuildingId.IsNullOrEmpty() || entity.BuildingId.Any(y => x.BuildingId != null && x.BuildingId == y);
            Expression<Func<LaundryOrder, bool>> storeId = x => entity.StoreId.IsNullOrEmpty() || entity.StoreId.Any(y => x.StoreId != null && x.StoreId == y);
            Expression<Func<LaundryOrder, bool>> note = x => entity.Note.IsNullOrEmpty() || entity.Note.Any(y => !x.Note.IsNullOrEmpty() && x.Note.Contains(y));
            Expression<Func<LaundryOrder, bool>> date = x => x.CreationDate.IsInDateTime(entity);
            var predicates = ExpressionUtils.CreateListOfExpression(customerId,buildingId,storeId,note,date);
            var seed =  Includes(_dbSet.AsNoTracking(), x => x.Building, x => x.OrderDetails, x => x.OrderInBatches, x => x.Customer, x => x.Store, x => x.Payments);
            var result = predicates.Aggregate(seed.AsEnumerable(), (a, b) => a.Where(b.Compile()));
            return result.AsEnumerable();
        }

       
    }
}
