using System;
using System.Linq;
using System.Reflection;

namespace Jaywapp.Infrastructure.Attributes
{
    /// <summary>
    /// <see cref="FilterableAttribute"/>에 대한 확장 메서드를 제공합니다.
    /// </summary>
    public static class FilterableAttributeExtensions
    {
        /// <summary>
        /// Enum 값에서 <see cref="FilterableAttribute"/>를 가져옵니다.
        /// </summary>
        /// <param name="value">대상 Enum 값입니다.</param>
        /// <param name="attr">찾은 어트리뷰트입니다.</param>
        /// <returns>어트리뷰트가 존재하면 true를 반환합니다.</returns>
        public static bool TryGetFilterableTargetField(this Enum value, out FilterableAttribute attr)
        {
            var field = value.GetType().GetField(value.ToString());
            attr = field?.GetCustomAttribute<FilterableAttribute>();
            return attr != null;
        }

        /// <summary>
        /// Enum 값이 지정된 필터링 유형의 대상 필드인지 확인합니다.
        /// </summary>
        /// <param name="value">대상 Enum 값입니다.</param>
        /// <param name="type">필터링 유형입니다.</param>
        /// <returns>대상 필드이면 true를 반환합니다.</returns>
        public static bool IsTargetField(this Enum value, eFilteringType type)
        {
            var field = value.GetType().GetField(value.ToString());
            if (field == null)
                return false;

            var attrs = field.GetCustomAttributes<FilterableAttribute>().ToList();

            foreach (var attr in attrs)
            {
                if (attr.Type == type)
                    return true;
            }

            return false;
        }
    }
}
