using System.Collections.Generic;
using System.Linq;

namespace Jaywapp.Infrastructure.Helpers
{
    /// <summary>
    /// <see cref="HashSet{T}"/>와 관련된 유틸리티 메서드를 제공하는 정적 클래스입니다.
    /// </summary>
    public static class HashSetHelper
    {
        /// <summary>
        /// 다른 컬렉션의 모든 요소를 <see cref="HashSet{T}"/>에 추가합니다.
        /// <see cref="HashSet{T}"/>의 고유성(uniqueness) 특성 때문에 중복된 요소는 자동으로 무시됩니다.
        /// </summary>
        /// <typeparam name="T">해시 집합의 요소 타입입니다.</typeparam>
        /// <param name="hashSet">확장 메서드가 적용될 대상 <see cref="HashSet{T}"/>입니다.</param>
        /// <param name="targets">해시 집합에 추가할 요소들을 담고 있는 컬렉션입니다.</param>
        /// <remarks>
        /// 이 메서드는 내부적으로 <see cref="HashSet{T}.Add(T)"/>를 반복하여 호출합니다.
        /// </remarks>
        public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> targets)
        {
            foreach (var target in targets.ToList())
                hashSet.Add(target);
        }

        /// <summary>
        /// 다른 컬렉션의 모든 요소를 <see cref="HashSet{T}"/>에서 제거합니다.
        /// </summary>
        /// <typeparam name="T">해시 집합의 요소 타입입니다.</typeparam>
        /// <param name="hashSet">확장 메서드가 적용될 대상 <see cref="HashSet{T}"/>입니다.</param>
        /// <param name="targets">해시 집합에서 제거할 요소들을 담고 있는 컬렉션입니다.</param>
        /// <remarks>
        /// 이 메서드는 내부적으로 <see cref="HashSet{T}.Remove(T)"/>를 반복하여 호출합니다.
        /// </remarks>
        public static void RemoveRange<T>(this HashSet<T> hashSet, IEnumerable<T> targets)
        {
            foreach (var target in targets.ToList())
                hashSet.Remove(target);
        }
    }
}
