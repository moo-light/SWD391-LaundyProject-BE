using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.FilterModels
{
    public class BatchFilteringModel :BaseFilterringModel
    {
        public Guid?[]? DriverId { get; set; } 
        public string? Type { get; set; }
        public string? Status { get; set; }
    }
}
