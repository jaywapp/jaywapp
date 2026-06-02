using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaywapp.Common.Extensions
{
    /// <summary>
    /// 컬렉션 및 LINQ 관련 확장 메서드를 제공합니다.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// 컬렉션이 비어 있는지 확인합니다.
        /// </summary>
        /// <typeparam name="T">요소의 타입입니다.</typeparam>
        /// <param name="source">확인할 컬렉션입니다.</param>
        /// <returns>컬렉션이 비어 있으면 true를 반환합니다.</returns>
        /// <exception cref="ArgumentNullException">source가 null인 경우 발생합니다.</exception>
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return !source.Any();
        }

        /// <summary>
        /// 컬렉션에 요소가 없는지 확인합니다.
        /// </summary>
        /// <typeparam name="T">요소의 타입입니다.</typeparam>
        /// <param name="source">확인할 컬렉션입니다.</param>
        /// <returns>요소가 없으면 true를 반환합니다.</returns>
        /// <exception cref="ArgumentNullException">source가 null인 경우 발생합니다.</exception>
        public static bool None<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return !source.Any();
        }

        /// <summary>
        /// 컬렉션에서 조건을 만족하는 요소가 없는지 확인합니다.
        /// </summary>
        /// <typeparam name="T">요소의 타입입니다.</typeparam>
        /// <param name="source">확인할 컬렉션입니다.</param>
        /// <param name="predicate">조건 함수입니다.</param>
        /// <returns>조건을 만족하는 요소가 없으면 true를 반환합니다.</returns>
        /// <exception cref="ArgumentNullException">source 또는 predicate가 null인 경우 발생합니다.</exception>
        public static bool None<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return !source.Any(predicate);
        }

        /// <summary>
        /// 컬렉션의 각 요소에 대해 액션을 안전하게 수행합니다.
        /// source가 null이면 아무 작업도 수행하지 않습니다.
        /// </summary>
        /// <typeparam name="T">요소의 타입입니다.</typeparam>
        /// <param name="source">대상 컬렉션입니다.</param>
        /// <param name="action">수행할 액션입니다.</param>
        /// <exception cref="ArgumentNullException">action이 null인 경우 발생합니다.</exception>
        public static void SafeForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (source == null)
                return;

            foreach (var item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// 컬렉션을 지정된 크기의 배치로 분할합니다.
        /// </summary>
        /// <typeparam name="T">요소의 타입입니다.</typeparam>
        /// <param name="source">분할할 컬렉션입니다.</param>
        /// <param name="size">배치 크기입니다.</param>
        /// <returns>배치로 분할된 컬렉션입니다.</returns>
        /// <exception cref="ArgumentNullException">source가 null인 경우 발생합니다.</exception>
        /// <exception cref="ArgumentOutOfRangeException">size가 0 이하인 경우 발생합니다.</exception>
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int size)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (size <= 0)
                throw new ArgumentOutOfRangeException(nameof(size), "배치 크기는 1 이상이어야 합니다.");

            return BatchIterator(source, size);
        }

        private static IEnumerable<IEnumerable<T>> BatchIterator<T>(IEnumerable<T> source, int size)
        {
            var batch = new List<T>(size);

            foreach (var item in source)
            {
                batch.Add(item);

                if (batch.Count == size)
                {
                    yield return batch;
                    batch = new List<T>(size);
                }
            }

            if (batch.Count > 0)
            {
                yield return batch;
            }
        }

        /// <summary>
        /// 지정된 키 선택자를 사용하여 중복 요소를 제거합니다.
        /// </summary>
        /// <typeparam name="T">요소의 타입입니다.</typeparam>
        /// <typeparam name="TKey">키의 타입입니다.</typeparam>
        /// <param name="source">대상 컬렉션입니다.</param>
        /// <param name="keySelector">키 선택 함수입니다.</param>
        /// <returns>중복이 제거된 컬렉션입니다.</returns>
        /// <exception cref="ArgumentNullException">source 또는 keySelector가 null인 경우 발생합니다.</exception>
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return DistinctByIterator(source, keySelector);
        }

        private static IEnumerable<T> DistinctByIterator<T, TKey>(IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            var seen = new HashSet<TKey>();

            foreach (var item in source)
            {
                if (seen.Add(keySelector(item)))
                {
                    yield return item;
                }
            }
        }
    }
}
