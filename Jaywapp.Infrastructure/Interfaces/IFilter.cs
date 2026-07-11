namespace Jaywapp.Infrastructure.Interfaces
{
    /// <summary>
    /// 필터 인터페이스입니다.
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// 다른 필터와의 연관 관계 연산자입니다.
        /// </summary>
        eLogicalOperator Logical { get; }

        /// <summary>
        /// <paramref name="target"/> 객체가 해당 필터에 걸리는지 확인합니다.
        /// </summary>
        /// <param name="target">확인할 대상 객체입니다.</param>
        /// <returns>필터 조건에 일치하면 true를 반환합니다.</returns>
        bool IsFiltered(object target);
    }
}
