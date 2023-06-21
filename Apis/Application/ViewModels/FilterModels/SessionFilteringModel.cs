﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.FilterModels
{
    public class SessionFilteringModel : BaseFilterringModel
    {
        public Guid?[]? BatchId { get; set; }
        public Guid?[]? BuildingId { get; set; }
    }
}
