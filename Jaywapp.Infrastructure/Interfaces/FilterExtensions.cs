using System.Collections.Generic;
using System.Linq;

namespace Jaywapp.Infrastructure.Interfaces
{
    /// <summary>
    /// <see cref="IFilter"/>에 대한 확장 메서드를 제공합니다.
    /// </summary>
    public static class FilterExtensions
    {
        /// <summary>
        /// <paramref name="filters"/>에 대해 <paramref name="target"/>이 필터 조건에 일치하는지 확인합니다.
        /// </summary>
        /// <param name="filters">필터 목록입니다.</param>
        /// <param name="target">확인할 대상 객체입니다.</param>
        /// <returns>필터 조건에 일치하면 true를 반환합니다.</returns>
        public static bool IsFiltered(this IEnumerable<IFilter> filters, object target)
        {
            var first = filters.FirstOrDefault();
            if (first == null)
                return false;

            var result = first.IsFiltered(target);

            foreach (var filter in filters.Skip(1).ToList())
            {
                if (filter.Logical == eLogicalOperator.AND)
                    result &= filter.IsFiltered(target);
                else if (filter.Logical == eLogicalOperator.OR)
                    result |= filter.IsFiltered(target);
            }

            return result;
        }
    }
}
