using Application.ViewModels.FilterModels;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IOrderInBatchRepository : IGenericRepository<OrderInBatch>
    {
        IEnumerable<OrderInBatch> GetFilter(OrderInBatchFilteringModel entity);
    }
}