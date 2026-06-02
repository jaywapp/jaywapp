using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaywapp.Common.Models
{
    /// <summary>
    /// 페이징 요청 정보를 나타내는 불변 클래스입니다.
    /// </summary>
    public class PageRequest
    {
        /// <summary>
        /// 페이지 번호입니다(1부터 시작).
        /// </summary>
        public int Page { get; }

        /// <summary>
        /// 페이지 크기입니다.
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// 정렬 정의 목록입니다.
        /// </summary>
        public IReadOnlyList<SortDefinition> Sorts { get; }

        /// <summary>
        /// 건너뛸 항목 수입니다.
        /// </summary>
        public int Skip => (Page - 1) * PageSize;

        /// <summary>
        /// <see cref="PageRequest"/>의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="page">페이지 번호입니다(1 이상).</param>
        /// <param name="pageSize">페이지 크기입니다(1 이상).</param>
        /// <param name="sorts">정렬 정의 목록입니다.</param>
        /// <exception cref="ArgumentOutOfRangeException">page 또는 pageSize가 1 미만인 경우 발생합니다.</exception>
        public PageRequest(int page, int pageSize, IEnumerable<SortDefinition> sorts = null)
        {
            if (page < 1)
                throw new ArgumentOutOfRangeException(nameof(page), "페이지 번호는 1 이상이어야 합니다.");
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "페이지 크기는 1 이상이어야 합니다.");

            Page = page;
            PageSize = pageSize;
            Sorts = sorts != null
                ? sorts.ToList().AsReadOnly()
                : new List<SortDefinition>().AsReadOnly();
        }
    }
}
