using System;

namespace Jaywapp.Common.Models
{
    /// <summary>
    /// 날짜 기간을 나타내는 불변 클래스입니다.
    /// 시작/종료 경계의 포함 여부를 지원합니다.
    /// </summary>
    public class DatePeriod
    {
        /// <summary>
        /// 기간의 시작 날짜입니다.
        /// </summary>
        public DateTime Start { get; }

        /// <summary>
        /// 기간의 종료 날짜입니다.
        /// </summary>
        public DateTime End { get; }

        /// <summary>
        /// 시작 날짜 포함 여부입니다.
        /// </summary>
        public bool IsStartInclusive { get; }

        /// <summary>
        /// 종료 날짜 포함 여부입니다.
        /// </summary>
        public bool IsEndInclusive { get; }

        /// <summary>
        /// 기간의 길이입니다.
        /// </summary>
        public TimeSpan Duration => End - Start;

        /// <summary>
        /// <see cref="DatePeriod"/>의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="start">시작 날짜입니다.</param>
        /// <param name="end">종료 날짜입니다.</param>
        /// <param name="isStartInclusive">시작 날짜 포함 여부입니다. 기본값은 true입니다.</param>
        /// <param name="isEndInclusive">종료 날짜 포함 여부입니다. 기본값은 true입니다.</param>
        /// <exception cref="ArgumentException">start가 end보다 큰 경우 발생합니다.</exception>
        public DatePeriod(DateTime start, DateTime end, bool isStartInclusive = true, bool isEndInclusive = true)
        {
            if (start > end)
                throw new ArgumentException("시작 날짜가 종료 날짜보다 클 수 없습니다.");

            Start = start;
            End = end;
            IsStartInclusive = isStartInclusive;
            IsEndInclusive = isEndInclusive;
        }

        /// <summary>
        /// 지정된 날짜가 기간 내에 있는지 확인합니다.
        /// </summary>
        /// <param name="value">확인할 날짜입니다.</param>
        /// <returns>기간 내에 있으면 true를 반환합니다.</returns>
        public bool Contains(DateTime value)
        {
            bool afterStart = IsStartInclusive ? value >= Start : value > Start;
            bool beforeEnd = IsEndInclusive ? value <= End : value < End;

            return afterStart && beforeEnd;
        }
    }
}
