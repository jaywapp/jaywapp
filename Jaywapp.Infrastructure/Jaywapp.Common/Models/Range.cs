using System;

namespace Jaywapp.Common.Models
{
    /// <summary>
    /// 비교 가능한 값의 범위를 나타내는 불변 클래스입니다.
    /// </summary>
    /// <typeparam name="T">비교 가능한 값의 타입입니다.</typeparam>
    public class Range<T> where T : IComparable<T>
    {
        /// <summary>
        /// 범위의 최솟값입니다.
        /// </summary>
        public T Min { get; }

        /// <summary>
        /// 범위의 최댓값입니다.
        /// </summary>
        public T Max { get; }

        /// <summary>
        /// <see cref="Range{T}"/>의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="min">최솟값입니다.</param>
        /// <param name="max">최댓값입니다.</param>
        /// <exception cref="ArgumentException">min이 max보다 큰 경우 발생합니다.</exception>
        public Range(T min, T max)
        {
            if (min == null)
                throw new ArgumentNullException(nameof(min));
            if (max == null)
                throw new ArgumentNullException(nameof(max));
            if (min.CompareTo(max) > 0)
                throw new ArgumentException("최솟값이 최댓값보다 클 수 없습니다.");

            Min = min;
            Max = max;
        }

        /// <summary>
        /// 지정된 값이 범위 내에 있는지 확인합니다(양쪽 경계 포함).
        /// </summary>
        /// <param name="value">확인할 값입니다.</param>
        /// <returns>범위 내에 있으면 true를 반환합니다.</returns>
        public bool Contains(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return value.CompareTo(Min) >= 0 && value.CompareTo(Max) <= 0;
        }

        /// <summary>
        /// 다른 범위와 겹치는지 확인합니다.
        /// </summary>
        /// <param name="other">비교할 범위입니다.</param>
        /// <returns>범위가 겹치면 true를 반환합니다.</returns>
        /// <exception cref="ArgumentNullException">other가 null인 경우 발생합니다.</exception>
        public bool Overlaps(Range<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            return !(other.Max.CompareTo(Min) < 0 || other.Min.CompareTo(Max) > 0);
        }
    }
}
