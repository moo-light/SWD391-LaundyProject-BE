using Application.ViewModels.FilterModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IFeedbackRepository : IGenericRepository<Feedback>
    {
        IEnumerable<Batch> GetFilter(FeedbackFilteringModel entity);
    }
}
