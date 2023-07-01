using Application.ViewModels.FilterModels;
using Application.ViewModels;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IOrderRepository : IGenericRepository<LaundryOrder>
    {
        IEnumerable<LaundryOrder> GetFilter(LaundryOrderFilteringModel entity);
    }
}
