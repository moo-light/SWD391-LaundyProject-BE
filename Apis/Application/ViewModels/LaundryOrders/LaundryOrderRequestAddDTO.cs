using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.LaundryOrders
{
    public class LaundryOrderRequestAddDTO
    {
        public int NumberOfPackages { get; set; }
        public Guid StoreId { get; set; }
        public Guid BuildingId { get; set; }
    }
}
