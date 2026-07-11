using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jaywapp.Infrastructure.Helpers
{
    public static class EnumHelper
    {
        /// <summary>
        /// Enum Type에서 모든 값을 추출합니다.
        /// </summary>
        /// <typeparam name="TEnum">Enum Type</typeparam>
        /// <returns></returns>
        public static IEnumerable<TEnum> GetValues<TEnum>()
            where TEnum : struct, IComparable
        {
            var enumType = typeof(TEnum);
            if (!enumType.IsEnum)
                return Enumerable.Empty<TEnum>();

            return Enum.GetValues(enumType).Cast<TEnum>();
        }

        /// <summary>
        /// Enum 값에 기술된 Description을 반환한다.
        /// </summary>
        /// <param name="value">Enum Value</param>
        /// <param name="result">Description</param>
        /// <returns>Description 유무</returns>
        public static bool TryGetDescription(Enum value, out string result)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field.GetCustomAttribute<DescriptionAttribute>();
            if (attribute != null)
            {
                result = attribute.Description;
                return true;
            }

            result = null;
            return false;
        }

        /// <summary>
        /// Enum 값에서 Description을 반환합다. 만약 Description이 설정되지 않은 경우 ToString 결과를 반환합니다.
        /// </summary>
        /// <param name="value">enum value</param>
        /// <returns>description 혹은 ToString</returns>
        public static string GetDescriptionOrToString(this Enum value)
        {
            if (TryGetDescription(value, out string result))
                return result;

            return value.ToString();
        }

        /// <summary>
        /// enum의 description에 해당하는 enum의 value 값을 리턴
        /// </summary>
        /// <typeparam name="TEnum">Enum</typeparam>
        /// <param name="description">Enum의 value의 Description</param>
        /// <returns> 못찾으면 default enum value를 리턴</returns>
        public static TEnum GetValueFromDescription<TEnum>(this string description)
            where TEnum : struct, IConvertible
        {
            var type = typeof(TEnum);
            if (!type.IsEnum) throw new InvalidOperationException();

            if (TryParseValueFromDescription(description, out TEnum result))
                return result;

            return default(TEnum);
        }

        /// <summary>
        /// enum의 description에 해당하는 enum의 value 값을 리턴
        /// </summary>
        /// <typeparam name="TENum">Enum</typeparam>
        /// <param name="description">Enum의 value의 Description</param>
        /// <param name="result">못찾으면 default enum value를 리턴</param>
        /// <returns>Parsing 성공 유무</returns>
        public static bool TryParseValueFromDescription<TENum>(string description, out TENum result)
            where TENum : struct, IConvertible
        {
            result = default(TENum);

            var type = typeof(TENum);
            if (!type.IsEnum) return false;

            foreach (var field in type.GetFields())
            {
                var attribute = field.GetCustomAttribute<DescriptionAttribute>();
                if (attribute?.Description == description || field.Name == description)
                {
                    result = (TENum)field.GetValue(null);
                    return true;
                }
            }

            return false;
        }

        public static bool TryParseValueFromDescription(string description, Type type, out object result)
        {
            result = default;

            if (!type.IsEnum)
                return false;

            foreach (var field in type.GetFields())
            {
                var attribute = field.GetCustomAttribute<DescriptionAttribute>();
                if (attribute?.Description == description || field.Name == description)
                {
                    result = field.GetValue(null);
                    return true;
                }
            }

            return false;
        }
    }
}
