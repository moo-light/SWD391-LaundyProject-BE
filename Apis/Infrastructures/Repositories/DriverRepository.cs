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
    public class DriverRepository : GenericRepository<Driver>, IDriverRepository
    {
        public DriverRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
        }

        public IEnumerable<Driver> GetFilter(DriverFilteringModel entity)
        {
            entity ??= new();
            Expression<Func<Driver, bool>> email = x => entity.Email.IsNullOrEmpty() || entity.Email.Any(y => x.Email != null && x.Email.Contains(y));
            Expression<Func<Driver, bool>> phoneNumber = x => entity.PhoneNumber.IsNullOrEmpty() || entity.PhoneNumber.Any(y => x.PhoneNumber != null && x.PhoneNumber.Contains(y));
            Expression<Func<Driver, bool>> fullName = x => entity.FullName.IsNullOrEmpty() || entity.FullName.Any(y => x.FullName != null && x.FullName.Contains(y));
            Expression<Func<Driver, bool>> date = x => x.CreationDate.IsInDateTime(entity);
            Expression<Func<Driver, bool>> status = x => entity.BatchStatus == null || x.Batches.Any(x => x.Status.Equals(entity.BatchStatus,StringComparison.OrdinalIgnoreCase));

            var predicates = ExpressionUtils.CreateListOfExpression(email, phoneNumber, fullName, date, status);
            IQueryable<Driver> seed = Includes(_dbSet.AsNoTracking(), x => x.Batches);

            IEnumerable<Driver> result = predicates.Aggregate(seed.AsEnumerable(), (a, b) => a.Where(b.Compile()));

            return result.ToList();
        }
    }
}
