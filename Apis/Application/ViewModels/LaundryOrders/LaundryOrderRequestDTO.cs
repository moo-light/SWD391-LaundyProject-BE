using Application.ViewModels.Buildings;
using Application.ViewModels.Customer;
using Application.ViewModels.OrderDetails;
using Application.ViewModels.OrderInBatch;
using Application.ViewModels.Payments;
using Application.ViewModels.Stores;
using Domain.Entities;

namespace Application.ViewModels.LaundryOrders
{
    public class LaundryOrderRequestDTO
    {
        public Guid? CustomerId { get; set; } = null;
        public Guid? StoreId { get; set; } = null;
        public Guid? BuildingId { get; set; } = null;
        public string? Note { get; set; } = null;
    }
}