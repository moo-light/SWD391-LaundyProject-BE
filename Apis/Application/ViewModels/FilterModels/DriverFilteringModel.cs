using Domain.CustomValidations;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.FilterModels
{
    public class DriverFilteringModel : UserFilteringModel
    {
        [EnumValidation(typeof(BatchStatus))]
        [AllowNull]
        public string? BatchStatus { get; set; }
    }
}
