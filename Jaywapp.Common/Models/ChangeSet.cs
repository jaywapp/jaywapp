using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaywapp.Common.Models
{
    /// <summary>
    /// 변경 사항 집합을 나타내는 불변 클래스입니다.
    /// 추가, 수정, 삭제된 항목을 추적합니다.
    /// </summary>
    /// <typeparam name="T">항목의 타입입니다.</typeparam>
    public class ChangeSet<T>
    {
        /// <summary>
        /// 추가된 항목 목록입니다.
        /// </summary>
        public IReadOnlyList<T> Added { get; }

        /// <summary>
        /// 수정된 항목 목록입니다.
        /// </summary>
        public IReadOnlyList<T> Updated { get; }

        /// <summary>
        /// 삭제된 항목 목록입니다.
        /// </summary>
        public IReadOnlyList<T> Removed { get; }

        /// <summary>
        /// 변경 사항이 있는지 여부입니다.
        /// </summary>
        public bool HasChanges => Added.Count > 0 || Updated.Count > 0 || Removed.Count > 0;

        /// <summary>
        /// 전체 변경 수입니다.
        /// </summary>
        public int TotalChanges => Added.Count + Updated.Count + Removed.Count;

        /// <summary>
        /// <see cref="ChangeSet{T}"/>의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="added">추가된 항목입니다.</param>
        /// <param name="updated">수정된 항목입니다.</param>
        /// <param name="removed">삭제된 항목입니다.</param>
        public ChangeSet(IEnumerable<T> added, IEnumerable<T> updated, IEnumerable<T> removed)
        {
            Added = (added ?? Enumerable.Empty<T>()).ToList().AsReadOnly();
            Updated = (updated ?? Enumerable.Empty<T>()).ToList().AsReadOnly();
            Removed = (removed ?? Enumerable.Empty<T>()).ToList().AsReadOnly();
        }

        /// <summary>
        /// 빈 변경 사항 집합을 생성합니다.
        /// </summary>
        /// <returns>빈 <see cref="ChangeSet{T}"/> 인스턴스입니다.</returns>
        public static ChangeSet<T> Empty()
        {
            return new ChangeSet<T>(null, null, null);
        }
    }
}
