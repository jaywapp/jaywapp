using System.ComponentModel;
using Jaywapp.Infrastructure.Attributes;

namespace Jaywapp.Infrastructure
{
    /// <summary>
    /// 필터링 연산자를 나타냅니다.
    /// </summary>
    public enum eFilteringOperator
    {
        /// <summary>
        /// 같음 연산입니다.
        /// </summary>
        [Filterable(eFilteringType.String)]
        [Filterable(eFilteringType.Number)]
        [Filterable(eFilteringType.Enum)]
        [Description("=")]
        Equal,

        /// <summary>
        /// 같지 않음 연산입니다.
        /// </summary>
        [Filterable(eFilteringType.String)]
        [Filterable(eFilteringType.Number)]
        [Filterable(eFilteringType.Enum)]
        [Description("\u2260")]
        NotEqual,

        /// <summary>
        /// 미만 연산입니다.
        /// </summary>
        [Filterable(eFilteringType.Number)]
        [Description("\uff1c")]
        LessThan,

        /// <summary>
        /// 이하 연산입니다.
        /// </summary>
        [Filterable(eFilteringType.Number)]
        [Description("\u2264")]
        LessEqual,

        /// <summary>
        /// 초과 연산입니다.
        /// </summary>
        [Filterable(eFilteringType.Number)]
        [Description("\uff1e")]
        GreaterThan,

        /// <summary>
        /// 이상 연산입니다.
        /// </summary>
        [Filterable(eFilteringType.Number)]
        [Description("\u2265")]
        GreaterEqual,

        /// <summary>
        /// 정규식 매칭 연산입니다.
        /// </summary>
        [Filterable(eFilteringType.String)]
        [Filterable(eFilteringType.Number)]
        [Filterable(eFilteringType.Enum)]
        [Description("정규식")]
        MatchRegex,

        /// <summary>
        /// 포함 연산입니다.
        /// </summary>
        [Filterable(eFilteringType.String)]
        [Filterable(eFilteringType.Number)]
        [Filterable(eFilteringType.Enum)]
        [Description("포함")]
        Contains,

        /// <summary>
        /// 미포함 연산입니다.
        /// </summary>
        [Filterable(eFilteringType.String)]
        [Filterable(eFilteringType.Number)]
        [Filterable(eFilteringType.Enum)]
        [Description("미포함")]
        NotContains,

        /// <summary>
        /// 시작 문자열 연산입니다.
        /// </summary>
        [Filterable(eFilteringType.String)]
        [Filterable(eFilteringType.Number)]
        [Filterable(eFilteringType.Enum)]
        [Description("시작 문자열")]
        StartsWith,

        /// <summary>
        /// 끝 문자열 연산입니다.
        /// </summary>
        [Filterable(eFilteringType.String)]
        [Filterable(eFilteringType.Number)]
        [Filterable(eFilteringType.Enum)]
        [Description("끝 문자열")]
        EndsWith,
    }
}
