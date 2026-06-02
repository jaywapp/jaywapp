using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaywapp.Common.Models
{
    /// <summary>
    /// 페이징 결과를 나타내는 불변 클래스입니다.
    /// </summary>
    /// <typeparam name="T">항목의 타입입니다.</typeparam>
    public class PageResult<T>
    {
        /// <summary>
        /// 현재 페이지의 항목 목록입니다.
        /// </summary>
        public IReadOnlyList<T> Items { get; }

        /// <summary>
        /// 전체 항목 수입니다.
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        /// 현재 페이지 번호입니다.
        /// </summary>
        public int Page { get; }

        /// <summary>
        /// 페이지 크기입니다.
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// 전체 페이지 수입니다.
        /// </summary>
        public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;

        /// <summary>
        /// 다음 페이지가 있는지 여부입니다.
        /// </summary>
        public bool HasNextPage => Page < TotalPages;

        /// <summary>
        /// 이전 페이지가 있는지 여부입니다.
        /// </summary>
        public bool HasPreviousPage => Page > 1;

        /// <summary>
        /// <see cref="PageResult{T}"/>의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="items">현재 페이지 항목입니다.</param>
        /// <param name="totalCount">전체 항목 수입니다.</param>
        /// <param name="page">현재 페이지 번호입니다.</param>
        /// <param name="pageSize">페이지 크기입니다.</param>
        /// <exception cref="ArgumentNullException">items가 null인 경우 발생합니다.</exception>
        public PageResult(IEnumerable<T> items, int totalCount, int page, int pageSize)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            Items = items.ToList().AsReadOnly();
            TotalCount = totalCount;
            Page = page;
            PageSize = pageSize;
        }
    }
}
