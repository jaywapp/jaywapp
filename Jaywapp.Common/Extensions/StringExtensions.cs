using System;
using System.Security.Cryptography;
using System.Text;

namespace Jaywapp.Common.Extensions
{
    /// <summary>
    /// 문자열 관련 확장 메서드를 제공합니다.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 문자열이 null이 아니고 공백이 아닌 유효한 값을 가지는지 확인합니다.
        /// </summary>
        /// <param name="value">확인할 문자열입니다.</param>
        /// <returns>유효한 값이 있으면 true, 그렇지 않으면 false를 반환합니다.</returns>
        public static bool HasValue(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// 문자열을 지정된 최대 길이로 안전하게 잘라냅니다.
        /// </summary>
        /// <param name="value">잘라낼 문자열입니다.</param>
        /// <param name="maxLength">최대 길이입니다.</param>
        /// <returns>잘라낸 문자열을 반환합니다. null이면 null을 반환합니다.</returns>
        /// <exception cref="ArgumentOutOfRangeException">maxLength가 0 미만인 경우 발생합니다.</exception>
        public static string SafeTruncate(this string value, int maxLength)
        {
            if (maxLength < 0)
                throw new ArgumentOutOfRangeException(nameof(maxLength), "최대 길이는 0 이상이어야 합니다.");

            if (value == null)
                return null;

            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        /// <summary>
        /// 문자열의 SHA256 해시를 소문자 16진수 문자열로 반환합니다.
        /// </summary>
        /// <param name="value">해시할 문자열입니다.</param>
        /// <returns>SHA256 해시 문자열입니다.</returns>
        /// <exception cref="ArgumentNullException">value가 null인 경우 발생합니다.</exception>
        public static string ToSha256(this string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(value);
                var hash = sha256.ComputeHash(bytes);
                var builder = new StringBuilder(hash.Length * 2);

                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        /// <summary>
        /// 줄 바꿈 문자를 지정된 형식으로 통일합니다.
        /// </summary>
        /// <param name="value">변환할 문자열입니다.</param>
        /// <param name="newLine">사용할 줄 바꿈 문자입니다. 기본값은 "\n"입니다.</param>
        /// <returns>줄 바꿈이 통일된 문자열을 반환합니다. null이면 null을 반환합니다.</returns>
        public static string NormalizeLineEndings(this string value, string newLine = "\n")
        {
            if (value == null)
                return null;

            return value.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", newLine);
        }
    }
}
