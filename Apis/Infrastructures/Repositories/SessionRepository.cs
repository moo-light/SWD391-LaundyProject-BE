using Application.ViewModels.FilterModels;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Utils;
using Application.ViewModels;
using Domain.Entities;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories
{
    public class SessionRepository : GenericRepository<BatchOfBuilding>, ISessionRepository
    {
        private readonly AppDbContext _dbContext;

        public SessionRepository(AppDbContext dbContext,
            ICurrentTime timeService,
            IClaimsService claimsService)
            : base(dbContext,
                  timeService,
                  claimsService)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<BatchOfBuilding> GetFilter(SessionFilteringModel entity)
        {
            entity ??= new();
            IEnumerable<BatchOfBuilding> result;
            Expression<Func<BatchOfBuilding, bool>> batchId = x => entity.BatchId.EmptyOrEquals(x.BatchId);
            Expression<Func<BatchOfBuilding, bool>> buildingId = x => entity.BuildingId.EmptyOrEquals(x.BuildingId);
            Expression<Func<BatchOfBuilding, bool>> date = x => x.CreationDate.IsInDateTime(entity);

            var predicates = ExpressionUtils.CreateListOfExpression(batchId, buildingId, date);
            var seed = Includes(_dbSet.AsNoTracking(), x => x.Building, x => x.Batch);

            result = predicates.Aggregate(_dbSet.AsEnumerable(), (a, b) => a.Where(b.Compile()));

            return result;
        }

     
    }
}
