using System;

namespace Jaywapp.Infrastructure.Attributes
{
    /// <summary>
    /// 필터링 가능한 필드를 나타내는 어트리뷰트입니다.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class FilterableAttribute : System.Attribute
    {
        /// <summary>
        /// 필터링 대상의 데이터 유형입니다.
        /// </summary>
        public eFilteringType Type { get; }

        /// <summary>
        /// <see cref="FilterableAttribute"/>의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="type">필터링 유형입니다.</param>
        public FilterableAttribute(eFilteringType type)
        {
            Type = type;
        }
    }
}
