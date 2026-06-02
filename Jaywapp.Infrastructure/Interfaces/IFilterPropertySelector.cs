namespace Jaywapp.Infrastructure.Interfaces
{
    public interface IFilterPropertySelector
    {
        /// <summary>
        /// 이름
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 필터링 유형
        /// </summary>
        eFilteringType Type { get; }
        /// <summary>
        /// <paramref name="target"/>에서 필터링할 항목을 수집합니다.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        object Select(object target);
    }
}
