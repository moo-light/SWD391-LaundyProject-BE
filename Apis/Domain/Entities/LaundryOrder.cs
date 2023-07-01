using Domain.Entitiess;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities;

public partial class LaundryOrder : BaseEntity
{

    public Guid? CustomerId { get; set; } = null;
    public Guid? StoreId { get; set; } = null;
    public Guid? BuildingId { get; set; } = null;
    [AllowNull]
    public string? Note { get; set; } = null;
    public virtual Customer? Customer { get; set; }
    public virtual Building? Building{ get; set; }
    public virtual Store? Store { get; set; } 
    public virtual ICollection<OrderInBatch> OrderInBatches { get; set; } = new List<OrderInBatch>();
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

}
