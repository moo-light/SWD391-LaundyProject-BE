using Application.Commons;
using Application.ViewModels.FilterModels;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Utils;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class BaseUserRepository : GenericRepository<BaseUser>, IBaseUserRepository
    {
        public BaseUserRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
        }

        public async Task<bool> CheckEmailExisted(string email)
        {
            return await _dbSet.AsNoTracking().AnyAsync(x => x.Email == email);
        }

        public IEnumerable<BaseUser> GetFilter(UserFilteringModel? entity)
        {
            entity ??= new();
            //Expression<Func<BaseUser, bool>> address = x => entity.Search.EmptyOrContainedIn(x.);
            Expression<Func<BaseUser, bool>> email = x => entity.Email.IsNullOrEmpty() || entity.Email.Any(y => !x.Email.IsNullOrEmpty() && x.Email.Contains(y));
            Expression<Func<BaseUser, bool>> phoneNumber = x => entity.PhoneNumber.IsNullOrEmpty() || entity.PhoneNumber.Any(y => !x.PhoneNumber.IsNullOrEmpty() && x.PhoneNumber.Contains(y));
            Expression<Func<BaseUser, bool>> fullName = x => entity.FullName.IsNullOrEmpty() || entity.FullName.Any(y => !x.FullName.IsNullOrEmpty() && x.FullName.Contains(y));

            var predicates = ExpressionUtils.CreateListOfExpression( email, phoneNumber, fullName);

            var result = predicates.Aggregate(_dbSet.AsEnumerable(), (a, b) => a.Where(b.Compile()));

            return result.AsQueryable();
        }

        public async Task<BaseUser?> GetUserByEmailAndPasswordHash(string email, string password)
        {
                BaseUser? user = (await GetAllAsync()).FirstOrDefault(x => x.Email == email && password.CheckPassword(x.PasswordHash));
                return user ?? throw new Exception("Email or password is not correct");
        }

  
    }
}
