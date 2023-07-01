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
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly AppDbContext _appDbContext;
        

        public CustomerRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
            _appDbContext = context;
            
        }

        public async Task<Customer> GetCustomerByEmailAndPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        public  IEnumerable<Customer> GetFilter(CustomerFilteringModel entity)
        {
            entity ??= new();
            Expression<Func<Customer, bool>> address = x => entity.Address.IsNullOrEmpty()|| entity.Address.Any(y=> x.Address != null && x.Address.Contains(y));
            Expression<Func<Customer, bool>> email = x => entity.Email.IsNullOrEmpty() || entity.Email.Any(y => x.Email != null && x.Email.Contains(y));
            Expression<Func<Customer, bool>> phoneNumber = x => entity.PhoneNumber.IsNullOrEmpty() || entity.PhoneNumber.Any(y => x.PhoneNumber != null && x.PhoneNumber.Contains(y));
            Expression<Func<Customer, bool>> fullName = x => entity.FullName.IsNullOrEmpty() || entity.FullName.Any(y =>x.FullName!=null&&  x.FullName.Contains(y));
            Expression<Func<Customer, bool>> date = x => x.CreationDate.IsInDateTime(entity);
            var predicates = ExpressionUtils.CreateListOfExpression(address, email, phoneNumber, fullName,date);
            
            IQueryable<Customer> seed = Includes(_dbSet.AsNoTracking(), x => x.Feedbacks, x => x.Orders);
            IEnumerable<Customer> result = predicates.Aggregate(seed.AsEnumerable(), (a, b) => a.Where(b.Compile()));

            return result;
        }

    }
}
