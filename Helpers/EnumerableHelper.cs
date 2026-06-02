using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Jaywapp.Infrastructure.Helpers
{
    public static class EnumerableHelper
    {
        /// <summary>
        /// <paramref name="collection"/>가 null이거나 빈 목록인지 확인합니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        /// <summary>
        /// IEnumerable<T> 컬렉션에서 null이 아닌 요소만 필터링하여 반환합니다.
        /// </summary>
        /// <typeparam name="T">컬렉션 요소의 타입입니다.</typeparam>
        /// <param name="enumerable">확장 메서드가 적용될 소스 컬렉션입니다.</param>
        /// <returns>null 요소를 제외한 새로운 IEnumerable<T> 컬렉션입니다.</returns>
        /// <remarks>
        /// 이 메서드는 LINQ의 Where 메서드를 사용하여 컬렉션의 각 요소를 확인하고,
        /// null이 아닌 요소만 포함하는 새로운 컬렉션을 지연 실행(lazy evaluation) 방식으로 생성합니다.
        /// </remarks>
        public static IEnumerable<T> IgnoreNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Where(x => x != null);
        }

        /// <summary>
        /// <typeparamref name="T"/> 목록을 <see cref="ObservableCollection{T}"/>형태로 변환하여 반환합니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            return new ObservableCollection<T>(enumerable);
        }

        /// <summary>
        /// Item 목록을 다음 Item과 페어링합니다.
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IEnumerable<(TItem, TItem)> ChainPairing<TItem>(this IEnumerable<TItem> items, bool isCircular = false)
        {
            var itemList = items.ToList();

            if (itemList.Count >= 2)
            {
                for (int idx = 0; idx < itemList.Count - 1; idx++)
                {
                    yield return (itemList[idx], itemList[idx + 1]);
                }

                if (isCircular)
                    yield return (items.Last(), items.First());
            }
        }
    }
}
