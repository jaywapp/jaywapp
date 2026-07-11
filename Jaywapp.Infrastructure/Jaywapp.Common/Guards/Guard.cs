using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaywapp.Common.Guards
{
    /// <summary>
    /// 인수 검증을 위한 가드 메서드를 제공합니다.
    /// 조건 불충족 시 즉시 예외를 발생시킵니다.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// 값이 null이 아닌지 검증합니다.
        /// </summary>
        /// <typeparam name="T">검증할 값의 타입입니다.</typeparam>
        /// <param name="value">검증할 값입니다.</param>
        /// <param name="paramName">파라미터 이름입니다.</param>
        /// <returns>검증을 통과한 값을 반환합니다.</returns>
        /// <exception cref="ArgumentNullException">value가 null인 경우 발생합니다.</exception>
        public static T NotNull<T>(T value, string paramName) where T : class
        {
            if (value == null)
                throw new ArgumentNullException(paramName);

            return value;
        }

        /// <summary>
        /// 문자열이 null이거나 비어 있지 않은지 검증합니다.
        /// </summary>
        /// <param name="value">검증할 문자열입니다.</param>
        /// <param name="paramName">파라미터 이름입니다.</param>
        /// <returns>검증을 통과한 문자열을 반환합니다.</returns>
        /// <exception cref="ArgumentNullException">value가 null인 경우 발생합니다.</exception>
        /// <exception cref="ArgumentException">value가 빈 문자열인 경우 발생합니다.</exception>
        public static string NotNullOrEmpty(string value, string paramName)
        {
            if (value == null)
                throw new ArgumentNullException(paramName);
            if (value.Length == 0)
                throw new ArgumentException("값이 비어 있을 수 없습니다.", paramName);

            return value;
        }

        /// <summary>
        /// 문자열이 null이거나 공백으로만 구성되지 않았는지 검증합니다.
        /// </summary>
        /// <param name="value">검증할 문자열입니다.</param>
        /// <param name="paramName">파라미터 이름입니다.</param>
        /// <returns>검증을 통과한 문자열을 반환합니다.</returns>
        /// <exception cref="ArgumentNullException">value가 null인 경우 발생합니다.</exception>
        /// <exception cref="ArgumentException">value가 공백으로만 구성된 경우 발생합니다.</exception>
        public static string NotNullOrWhiteSpace(string value, string paramName)
        {
            if (value == null)
                throw new ArgumentNullException(paramName);
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("값이 공백으로만 구성될 수 없습니다.", paramName);

            return value;
        }

        /// <summary>
        /// 값이 지정된 범위 내에 있는지 검증합니다.
        /// </summary>
        /// <typeparam name="T">비교 가능한 값의 타입입니다.</typeparam>
        /// <param name="value">검증할 값입니다.</param>
        /// <param name="min">최솟값입니다.</param>
        /// <param name="max">최댓값입니다.</param>
        /// <param name="paramName">파라미터 이름입니다.</param>
        /// <returns>검증을 통과한 값을 반환합니다.</returns>
        /// <exception cref="ArgumentOutOfRangeException">값이 범위 밖인 경우 발생합니다.</exception>
        public static T InRange<T>(T value, T min, T max, string paramName) where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
                throw new ArgumentOutOfRangeException(paramName, value, $"값은 {min}에서 {max} 사이여야 합니다.");

            return value;
        }

        /// <summary>
        /// 컬렉션이 null이 아니고 비어 있지 않은지 검증합니다.
        /// </summary>
        /// <typeparam name="T">요소의 타입입니다.</typeparam>
        /// <param name="collection">검증할 컬렉션입니다.</param>
        /// <param name="paramName">파라미터 이름입니다.</param>
        /// <returns>검증을 통과한 컬렉션을 반환합니다.</returns>
        /// <exception cref="ArgumentNullException">collection이 null인 경우 발생합니다.</exception>
        /// <exception cref="ArgumentException">collection이 비어 있는 경우 발생합니다.</exception>
        public static IEnumerable<T> NotEmpty<T>(IEnumerable<T> collection, string paramName)
        {
            if (collection == null)
                throw new ArgumentNullException(paramName);
            if (!collection.Any())
                throw new ArgumentException("컬렉션이 비어 있을 수 없습니다.", paramName);

            return collection;
        }

        /// <summary>
        /// 조건이 true인지 검증합니다.
        /// </summary>
        /// <param name="condition">검증할 조건입니다.</param>
        /// <param name="message">조건 불충족 시 예외 메시지입니다.</param>
        /// <exception cref="InvalidOperationException">조건이 false인 경우 발생합니다.</exception>
        public static void Requires(bool condition, string message)
        {
            if (!condition)
                throw new InvalidOperationException(message);
        }
    }
}
