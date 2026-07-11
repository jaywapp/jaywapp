using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Jaywapp.Common.Extensions
{
    /// <summary>
    /// Enum 관련 확장 메서드를 제공합니다.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Enum 값의 <see cref="DescriptionAttribute"/> 값을 반환합니다.
        /// 설정되지 않은 경우 ToString() 결과를 반환합니다.
        /// </summary>
        /// <param name="value">대상 Enum 값입니다.</param>
        /// <returns>Description 또는 ToString() 결과입니다.</returns>
        public static string GetDescription(this Enum value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var field = value.GetType().GetField(value.ToString());
            if (field == null)
                return value.ToString();

            var attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute != null ? attribute.Description : value.ToString();
        }

        /// <summary>
        /// 문자열을 Enum 값으로 안전하게 파싱합니다.
        /// 실패 시 기본값을 반환합니다.
        /// </summary>
        /// <typeparam name="TEnum">Enum 타입입니다.</typeparam>
        /// <param name="value">파싱할 문자열입니다.</param>
        /// <param name="defaultValue">파싱 실패 시 반환할 기본값입니다.</param>
        /// <returns>파싱된 Enum 값 또는 기본값입니다.</returns>
        public static TEnum SafeParse<TEnum>(string value, TEnum defaultValue = default(TEnum))
            where TEnum : struct
        {
            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;

            if (Enum.TryParse(value, true, out TEnum result))
                return result;

            return defaultValue;
        }

        /// <summary>
        /// 문자열을 Enum 값으로 파싱합니다(대소문자 무시).
        /// </summary>
        /// <typeparam name="TEnum">Enum 타입입니다.</typeparam>
        /// <param name="value">파싱할 문자열입니다.</param>
        /// <param name="result">파싱 결과입니다.</param>
        /// <returns>파싱 성공 여부입니다.</returns>
        public static bool TryParseEnum<TEnum>(string value, out TEnum result)
            where TEnum : struct
        {
            return Enum.TryParse(value, true, out result);
        }

        /// <summary>
        /// Enum 타입의 모든 값을 반환합니다.
        /// </summary>
        /// <typeparam name="TEnum">Enum 타입입니다.</typeparam>
        /// <returns>Enum의 모든 값 컬렉션입니다.</returns>
        /// <exception cref="ArgumentException">TEnum이 Enum 타입이 아닌 경우 발생합니다.</exception>
        public static IEnumerable<TEnum> GetValues<TEnum>()
            where TEnum : struct
        {
            var enumType = typeof(TEnum);
            if (!enumType.IsEnum)
                throw new ArgumentException($"{enumType.Name}은(는) Enum 타입이 아닙니다.");

            return Enum.GetValues(enumType).Cast<TEnum>();
        }
    }
}
