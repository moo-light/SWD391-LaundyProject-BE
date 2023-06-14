﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Driver : BaseUser
    {
        public virtual ICollection<Batch> Batches { get; } = new List<Batch>();
    }
}
