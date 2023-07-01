using Application.Commons;
using Application.ViewModels;
using Domain.Entities;
using Domain.Entitiess;
using System.Linq.Expressions;

namespace Application.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<int> GetCountAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes);
        //IQueryable<TEntity> GetFilter(BaseFilterringModel entity);
        Task<bool> AddAsync(TEntity entity);
        bool Update(TEntity entity);
        bool UpdateRange(List<TEntity> entities);
        bool SoftRemove(TEntity entity);
        bool SoftRemoveByID(Guid entityId);

        Task<bool> AddRangeAsync(List<TEntity> entities);
        bool SoftRemoveRange(List<TEntity> entities);

        Task<Pagination<TEntity>> ToPagination(int pageNumber = 0, int pageSize = 10, params Expression<Func<TEntity, object>>[] includes);
        Pagination<TEntity> ToPagination(IEnumerable<TEntity> list,int pageNumber = 0, int pageSize = 10, params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> Includes(IQueryable<TEntity> entities,params Expression<Func<TEntity, object>>[] property);
    }
}
