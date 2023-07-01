using Application.ViewModels.OrderDetails;
using Domain.Entities;

namespace Application.ViewModels.Stores
{
    public class ServiceResponseDTO : ServiceRequestDTO
    {
        public Guid? ServiceId { get; set; }
        public virtual StoreResponseDTO? Store { get; set; } = null;
        public virtual ICollection<OrderDetailResponseDTO>? OrderDetails { get; set; } = null;
    }
}