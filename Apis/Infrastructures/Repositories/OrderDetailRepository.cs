using Application.ViewModels.FilterModels;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Utils;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;

namespace Infrastructures.Repositories
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {

        public OrderDetailRepository(AppDbContext dbContext,
            ICurrentTime timeService,
            IClaimsService claimsService)
            : base(dbContext,
                  timeService,
                  claimsService)
        {
        }

        public IEnumerable<OrderDetail> GetFilter(OrderDetailFilteringModel entity)
        {
            entity ??= new();

            Expression<Func<OrderDetail, bool>> orderId = x => entity.OrderId.IsNullOrEmpty() || entity.OrderId.Any(y => x.OrderId != null && x.OrderId == y);
            Expression<Func<OrderDetail, bool>> serviceId = x => entity.ServiceId.IsNullOrEmpty() || entity.ServiceId.Any(y => x.ServiceId != null && x.ServiceId == y);
            Expression<Func<OrderDetail, bool>> weight = x => entity.Weight.IsNullOrEmpty() || entity.Weight.Any(y => x.Weight.ToString().Contains(y));
            Expression<Func<OrderDetail, bool>> date = x => x.CreationDate.IsInDateTime(entity);
            Expression<Func<OrderDetail, bool>> status = x => x.Status.Equals(entity.Status,StringComparison.OrdinalIgnoreCase) ;
        
            var predicates = ExpressionUtils.CreateListOfExpression(orderId, serviceId, weight);
            var seed = Includes(_dbSet.AsNoTracking(), x => x.Order, x => x.Service);
            var result = predicates.Aggregate(seed.AsEnumerable(), (a, b) => a.Where(b.Compile()));

            return result;

        }

    }
}
