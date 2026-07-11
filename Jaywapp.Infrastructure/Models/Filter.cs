using System;
using System.Text.RegularExpressions;
using Jaywapp.Infrastructure.Interfaces;

namespace Jaywapp.Infrastructure.Models
{
    public class Filter : IFilter
    {
        #region Properties
        /// <inheritdoc/>
        public eLogicalOperator Logical { get; set; }

        /// <summary>
        /// Selector
        /// </summary>
        public IFilterPropertySelector Selector { get; set; }
        /// <summary>
        /// 연산자
        /// </summary>
        public eFilteringOperator Operator { get; set; }
        /// <summary>
        /// 기대값
        /// </summary>
        public string Expect { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public Filter()
        {
        }
        #endregion

        #region Functions
        /// <inheritdoc/>
        public bool IsFiltered(object target)
        {
            var actual = GetActual(target);
            var expect = GetExpect(Expect, actual.GetType());

            return Check(actual, expect, Operator);
        }

        /// <summary>
        /// <paramref name="target"/>에서 필터링에 필요한 값을 추출합니다.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private object GetActual(object target) => Selector?.Select(target);

        /// <summary>
        /// 입력받은 <paramref name="target"/> 문자열을 <paramref name="type"/>형으로 변환하여 반환합니다.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static object GetExpect(string target, Type type)
        {
            if (type == typeof(string))
                return target;
            else if (type == typeof(Enum))
                return Enum.Parse(type, target);
            else if (type == typeof(int))
                return int.Parse(target);
            else if (type == typeof(double))
                return double.Parse(target);

            return null;
        }

        /// <summary>
        /// <paramref name="actual"/>(실제값)과 <paramref name="expect"/>)기대값이 <paramref name="op"/>(연산자)에 대해 일치하는지 확인합니다.
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expect"></param>
        /// <param name="op"></param>
        /// <returns></returns>
        private bool Check(object actual, object expect, eFilteringOperator op)
        {
            if (actual is Enum actualEnum && expect is Enum expectEnum)
                return CheckEnum(actualEnum, expectEnum, op);
            else if (actual is string actualString && expect is string expectString)
                return CheckString(actualString, expectString, op);
            else if (actual is IComparable actualComparable && expect is IComparable expectComparable)
                return CheckNumber(actualComparable, expectComparable, op);

            return false;
        }

        /// <summary>
        /// <paramref name="actual"/>(실제값)과 <paramref name="expect"/>)기대값이 <paramref name="op"/>(연산자)에 대해 일치하는지 확인합니다.
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expect"></param>
        /// <param name="op"></param>
        /// <returns></returns>
        private static bool CheckEnum(Enum actual, Enum expect, eFilteringOperator op)
        {
            if (actual.GetType() != expect.GetType())
                return false;

            switch (op)
            {
                case eFilteringOperator.Equal:
                    return actual.Equals(expect);
                case eFilteringOperator.NotEqual:
                    return !actual.Equals(expect);
                case eFilteringOperator.MatchRegex:
                    return Regex.IsMatch(actual.ToString(), expect.ToString());
                case eFilteringOperator.Contains:
                    return actual.ToString().Contains(expect.ToString());
                case eFilteringOperator.NotContains:
                    return !actual.ToString().Contains(expect.ToString());
                case eFilteringOperator.StartsWith:
                    return actual.ToString().StartsWith(expect.ToString());
                case eFilteringOperator.EndsWith:
                    return actual.ToString().EndsWith(expect.ToString());
                default:
                    return false;
            }
        }

        /// <summary>
        /// <paramref name="actual"/>(실제값)과 <paramref name="expect"/>)기대값이 <paramref name="op"/>(연산자)에 대해 일치하는지 확인합니다.
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expect"></param>
        /// <param name="op"></param>
        /// <returns></returns>
        private static bool CheckNumber(IComparable actual, IComparable expect, eFilteringOperator op)
        {
            var result = actual.CompareTo(expect);

            switch (op)
            {
                case eFilteringOperator.Equal:
                    return result == 0;
                case eFilteringOperator.NotEqual:
                    return result != 0;
                case eFilteringOperator.LessThan:
                    return result < 0;
                case eFilteringOperator.LessEqual:
                    return result <= 0;
                case eFilteringOperator.GreaterThan:
                    return result > 0;
                case eFilteringOperator.GreaterEqual:
                    return result >= 0;
                case eFilteringOperator.MatchRegex:
                    return Regex.IsMatch(actual.ToString(), expect.ToString());
                case eFilteringOperator.Contains:
                    return actual.ToString().Contains(expect.ToString());
                case eFilteringOperator.NotContains:
                    return !actual.ToString().Contains(expect.ToString());
                case eFilteringOperator.StartsWith:
                    return actual.ToString().StartsWith(expect.ToString());
                case eFilteringOperator.EndsWith:
                    return actual.ToString().EndsWith(expect.ToString());
                default:
                    return false;
            }
        }

        /// <summary>
        /// <paramref name="actual"/>(실제값)과 <paramref name="expect"/>)기대값이 <paramref name="op"/>(연산자)에 대해 일치하는지 확인합니다.
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expect"></param>
        /// <param name="op"></param>
        /// <returns></returns>
        private static bool CheckString(object actual, object expect, eFilteringOperator op)
        {
            if (!(actual is string actualStr) || !(expect is string expectStr))
                return false;

            switch (op)
            {
                case eFilteringOperator.Equal:
                    return string.Equals(actualStr, expectStr);
                case eFilteringOperator.NotEqual:
                    return !string.Equals(actualStr, expectStr);
                case eFilteringOperator.MatchRegex:
                    return Regex.IsMatch(actualStr, expectStr);
                case eFilteringOperator.Contains:
                    return actualStr.Contains(expectStr);
                case eFilteringOperator.NotContains:
                    return !actualStr.Contains(expectStr);
                case eFilteringOperator.StartsWith:
                    return actualStr.StartsWith(expectStr);
                case eFilteringOperator.EndsWith:
                    return actualStr.EndsWith(expectStr);
                default:
                    return false;
            }
        }
        #endregion
    }
}
