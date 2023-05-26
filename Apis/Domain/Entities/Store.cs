﻿using Domain.Entitiess;
using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Store : BaseEntity
{

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public Guid OwnerId { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual User Owner { get; set; } = null!;

    public virtual ICollection<Service> Services { get; } = new List<Service>();

    public virtual ICollection<StoreReport> StoreReports { get; } = new List<StoreReport>();
}
