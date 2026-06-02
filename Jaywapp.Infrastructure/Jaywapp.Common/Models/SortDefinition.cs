using System;

namespace Jaywapp.Common.Models
{
    /// <summary>
    /// 정렬 정의를 나타내는 불변 클래스입니다.
    /// </summary>
    public class SortDefinition
    {
        /// <summary>
        /// 정렬할 필드 이름입니다.
        /// </summary>
        public string Field { get; }

        /// <summary>
        /// 정렬 방향입니다.
        /// </summary>
        public SortDirection Direction { get; }

        /// <summary>
        /// <see cref="SortDefinition"/>의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="field">정렬할 필드 이름입니다.</param>
        /// <param name="direction">정렬 방향입니다. 기본값은 오름차순입니다.</param>
        /// <exception cref="ArgumentException">field가 null이거나 비어 있는 경우 발생합니다.</exception>
        public SortDefinition(string field, SortDirection direction = SortDirection.Ascending)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentException("필드 이름은 비어 있을 수 없습니다.", nameof(field));

            Field = field;
            Direction = direction;
        }
    }
}
