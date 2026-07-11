using System;

namespace Jaywapp.Common.Extensions
{
    /// <summary>
    /// DateTime 관련 확장 메서드를 제공합니다.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 해당 날짜의 시작 시각(00:00:00)을 반환합니다.
        /// </summary>
        /// <param name="dateTime">대상 날짜입니다.</param>
        /// <returns>해당 날짜의 시작 시각입니다.</returns>
        public static DateTime StartOfDay(this DateTime dateTime)
        {
            return dateTime.Date;
        }

        /// <summary>
        /// 해당 날짜의 종료 시각(23:59:59.9999999)을 반환합니다.
        /// </summary>
        /// <param name="dateTime">대상 날짜입니다.</param>
        /// <returns>해당 날짜의 종료 시각입니다.</returns>
        public static DateTime EndOfDay(this DateTime dateTime)
        {
            return dateTime.Date.AddDays(1).AddTicks(-1);
        }

        /// <summary>
        /// 날짜가 지정된 범위 내에 있는지 확인합니다(시작, 종료 포함).
        /// </summary>
        /// <param name="dateTime">확인할 날짜입니다.</param>
        /// <param name="start">범위의 시작 날짜입니다.</param>
        /// <param name="end">범위의 종료 날짜입니다.</param>
        /// <returns>범위 내에 있으면 true를 반환합니다.</returns>
        public static bool IsInRange(this DateTime dateTime, DateTime start, DateTime end)
        {
            return dateTime >= start && dateTime <= end;
        }

        /// <summary>
        /// DateTime을 안전하게 UTC로 변환합니다.
        /// Unspecified는 UTC로 간주하고, Local은 UTC로 변환합니다.
        /// </summary>
        /// <param name="dateTime">변환할 DateTime입니다.</param>
        /// <returns>UTC DateTime입니다.</returns>
        public static DateTime ToUtcSafe(this DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Utc)
                return dateTime;

            if (dateTime.Kind == DateTimeKind.Unspecified)
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);

            return dateTime.ToUniversalTime();
        }

        /// <summary>
        /// DateTime을 안전하게 Local로 변환합니다.
        /// Unspecified는 Local로 간주하고, UTC는 Local로 변환합니다.
        /// </summary>
        /// <param name="dateTime">변환할 DateTime입니다.</param>
        /// <returns>Local DateTime입니다.</returns>
        public static DateTime ToLocalSafe(this DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Local)
                return dateTime;

            if (dateTime.Kind == DateTimeKind.Unspecified)
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Local);

            return dateTime.ToLocalTime();
        }
    }
}
