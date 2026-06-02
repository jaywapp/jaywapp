using System.Collections.Generic;

namespace Jaywapp.Common.Models
{
    /// <summary>
    /// 값의 변경을 추적하는 클래스입니다.
    /// 원본 값과 현재 값을 관리합니다.
    /// </summary>
    /// <typeparam name="T">추적할 값의 타입입니다.</typeparam>
    public class Trackable<T>
    {
        /// <summary>
        /// 원본 값입니다.
        /// </summary>
        public T Original { get; private set; }

        /// <summary>
        /// 현재 값입니다.
        /// </summary>
        public T Current { get; set; }

        /// <summary>
        /// 값이 변경되었는지 여부입니다.
        /// </summary>
        public bool IsDirty => !EqualityComparer<T>.Default.Equals(Original, Current);

        /// <summary>
        /// <see cref="Trackable{T}"/>의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="value">초기 값입니다.</param>
        public Trackable(T value)
        {
            Original = value;
            Current = value;
        }

        /// <summary>
        /// 현재 값을 원본으로 승인합니다.
        /// </summary>
        public void AcceptChanges()
        {
            Original = Current;
        }

        /// <summary>
        /// 현재 값을 원본으로 되돌립니다.
        /// </summary>
        public void RejectChanges()
        {
            Current = Original;
        }
    }
}
