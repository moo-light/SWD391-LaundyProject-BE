using Domain.Entitiess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public partial class BatchOfBuilding : BaseEntity
{
    public Guid? BatchId { get; set; }
    public Guid? BuildingId { get; set; }
    public virtual Batch? Batch { get; set; } 
    public virtual Building? Building { get; set; }
}
