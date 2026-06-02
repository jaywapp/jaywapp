using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Jaywapp.Infrastructure.Helpers
{
    public static class DataTableHelper
    {
        /// <summary>
        /// <see cref="DataColumnCollection"/>을 <see cref="List{T}"/>로 변환합니다.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static List<DataColumn> ToList(this DataColumnCollection collection)
        {
            var columns = new List<DataColumn>();
            foreach (DataColumn c in collection)
                columns.Add(c);

            return columns;
        }

        /// <summary>
        /// <see cref="DataRowCollection"/>을 <see cref="List{T}"/>로 변환합니다.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static List<DataRow> ToList(this DataRowCollection collection)
        {
            var rows = new List<DataRow>();
            foreach (DataRow c in collection)
                rows.Add(c);

            return rows;
        }
    }
}
