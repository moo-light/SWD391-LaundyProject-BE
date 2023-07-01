using Application.ViewModels.FilterModels;
using Domain.Entities;
using Domain.Enums;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utils
{
    public static class ExpressionUtils
    {

        public static List<Expression<Func<T, bool>>> CreateListOfExpression<T>(params Expression<Func<T, bool>>[]? expressions )
        {
            return expressions?.ToList() ?? new List<Expression<Func<T, bool>>>();
        }

        public static bool EmptyOrEquals(this Guid? thisId, Guid? thatId)
        {
            if (thisId == null || thisId == Guid.Empty) return true;

            if (thatId == null) return false;
            
            return thisId.Equals(thatId );
        }
        /// <summary>
        /// check if any of the list is equal thatId
        /// </summary>
        /// <param name="theseId"></param>
        /// <param name="thatId"></param>
        /// <returns>true if empty or one GUID equal thatId</returns>
        public static bool EmptyOrEquals(this Guid?[]? theseId, Guid? thatId)
        {
            if (theseId == null) return true;

            return theseId.Any(x => x != null && x.EmptyOrEquals(thatId));
        }
        public static bool EmptyOrContainedIn(this string? @this, string? that)
        {
            if (@this == null || @this == string.Empty) return true;
            if (that == null) return false;
            if (that.Contains(@this,StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }
        public static bool EmptyOrContainedIn(this string?[]? @this, string? that)
        {

            if (@this == null) return true;
            return @this.Any(x=>!x.IsNullOrEmpty() && x.EmptyOrContainedIn(that));
        }
        public static bool IsInDateTime(this DateTime? dateTime, DateTime? fromDate = default, DateTime? toDate = default)
        {
            if (dateTime == null) return false;
            DateTime timeStart = (fromDate ?? DateTime.MinValue);
            DateTime timeEnd = (toDate ?? DateTime.MaxValue);
            return timeStart <= dateTime && dateTime <= timeEnd;
        }
        public static bool IsInDateTime(this DateTime? dateTime, BaseFilterringModel entity)
        {
            return dateTime.IsInDateTime(entity.FromDate, entity.ToDate);
        }
        public static bool IsInEnumNames(this string current, string[]? enumNames, Type? enumType = null)
        {
            // Return false if current is null or empty
            if (string.IsNullOrEmpty(current)) return false;

            // No filter if enumNames is null, then all values are valid
            if (enumNames == null) return true;

            // Check if all values in enumNames are valid enum names
            if (enumType != null)
                if (enumNames.Any(name => !Enum.IsDefined(enumType, name))) return false;

            // Return true if current is in enumNames
            return enumNames.Contains(current);
        }
    }
}
