using Domain.CustomValidations;
using Domain.Entitiess;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Batch : BaseEntity
{

    public Guid? DriverId { get; set; }
    //public Guid? SessionId { get; set; }//??
    [EnumValidation(typeof(BatchType))]
    public string? Type { get; set; }
    [EnumValidation(typeof(BatchStatus))]
    public string? Status { get; set; }
    public DateTime? Date { get; set; }
    public virtual Driver? Driver { get; set; }
    public virtual ICollection<BatchOfBuilding> BatchOfBuildings { get; set; } = new List<BatchOfBuilding>();
    public virtual ICollection<OrderInBatch> OrderInBatches { get; set; } = new List<OrderInBatch>();
}
