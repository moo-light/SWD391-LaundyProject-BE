using Application.ViewModels.FilterModels;
using Application.ViewModels;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        IEnumerable<Service> GetFilter(ServiceFilteringModel entity);
    }
}