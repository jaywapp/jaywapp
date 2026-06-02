using System.Collections.Generic;
using Jaywapp.Infrastructure.Interfaces;

namespace Jaywapp.Infrastructure.Models
{
    public class FilterGroup : IFilter
    {
        #region Properties
        /// <inheritdoc/>
        public eLogicalOperator Logical { get; set; }

        /// <summary>
        /// 하위 필터 목록
        /// </summary>
        public List<IFilter> Children { get; set; } = new List<IFilter>();
        #endregion

        #region Functions
        /// <inheritdoc/>
        public bool IsFiltered(object obj) => Children.IsFiltered(obj);
        #endregion
    }
}
