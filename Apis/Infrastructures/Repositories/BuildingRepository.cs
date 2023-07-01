using Application.Commons;
using Application.ViewModels.FilterModels;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Utils;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories
{
    public class BuildingRepository : GenericRepository<Building>, IBuildingRepository
    {
        public BuildingRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {

        }

        public IEnumerable<Building> GetFilter(BuildingFilteringModel entity)
        {
            entity ??= new();
            Expression<Func<Building, bool>> nameFilter = x => entity.Name.IsNullOrEmpty() || entity.Name.Any(y => x.Name != null && x.Name.Contains(y));
            Expression<Func<Building, bool>> addressFilter = x => entity.Address.IsNullOrEmpty() || entity.Address.Any(y => x.Address != null && x.Address.Contains(y));
            Expression<Func<Building, bool>> dateFilter = x => x.CreationDate.IsInDateTime(entity.FromDate, entity.ToDate);
            var predicates = ExpressionUtils.CreateListOfExpression(nameFilter, addressFilter, dateFilter);
            var includes = new Expression<Func<Building, object>>[]
            {
                x=>x.BatchOfBuildings,
                x=>x.Orders
            };
            var seed = Includes(_dbSet.AsNoTracking(), includes);
            var result = predicates.Aggregate(seed.AsEnumerable(), (a, predicate) => a.Where(predicate.Compile()));
            return result;
        }
    }
}
