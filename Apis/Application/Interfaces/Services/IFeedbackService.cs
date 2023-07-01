using Application.Commons;
using Application.ViewModels.Buildings;
using Application.ViewModels.Feedbacks;
using Application.ViewModels.FilterModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IFeedbackService
    {
        Task<bool> AddAsync(FeedbackRequestDTO building);
        Task<IEnumerable<FeedbackResponseDTO>> GetAllAsync();
        Task<Feedback?> GetByIdAsync(Guid entityId);
        Task<int> GetCountAsync();
        Task<Pagination<Feedback>> GetFilterAsync(FeedbackFilteringModel entity);
        bool Remove(Guid entityId);
        Task<bool> Update(Guid id, FeedbackRequestDTO entity);
        Task<Pagination<FeedbackResponseDTO>> GetCustomerListPagi(int pageIndex, int pageSize);
        Task<Pagination<FeedbackResponseDTO>> GetFilterAsync(FeedbackFilteringModel customer, int pageIndex, int pageSize);
    }
}
