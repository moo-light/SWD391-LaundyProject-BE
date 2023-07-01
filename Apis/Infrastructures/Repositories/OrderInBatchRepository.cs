using Application.ViewModels.FilterModels;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Utils;
using Application.ViewModels;
using Domain.Entities;
using Infrastructures;
using Infrastructures.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Repositories;

public class OrderInBatchRepository : GenericRepository<OrderInBatch>, IOrderInBatchRepository
{

    public OrderInBatchRepository(AppDbContext dbContext,
        ICurrentTime timeService,
        IClaimsService claimsService)
        : base(dbContext,
              timeService,
              claimsService)
    {
    }

    public  IEnumerable<OrderInBatch> GetFilter(OrderInBatchFilteringModel entity)
    {



        entity ??= new();
        Expression<Func<OrderInBatch, bool>> batchId = x => entity.BatchId.IsNullOrEmpty() || entity.BatchId.Any(y => x.BatchId != null && x.BatchId == y);
        Expression<Func<OrderInBatch, bool>> orderId = x => entity.OrderId.IsNullOrEmpty() || entity.OrderId.Any(y => x.OrderId != null && x.OrderId == y);
        Expression<Func<OrderInBatch, bool>> status = x => entity.Status.IsNullOrEmpty() || entity.Status.Any(y => !x.Status.IsNullOrEmpty()&& x.Status.Contains(y));
        Expression<Func<OrderInBatch, bool>> date = x => x.CreationDate.IsInDateTime(entity);

        var predicates = ExpressionUtils.CreateListOfExpression(batchId,orderId,status,date);
        var seed = Includes(_dbSet.AsNoTracking(), x => x.Order, x => x.Batch);

        var query = seed.AsEnumerable();
        foreach (var predicate in predicates)
        {
            query = query.Where(predicate.Compile());
        }

        return query;

    }

}
