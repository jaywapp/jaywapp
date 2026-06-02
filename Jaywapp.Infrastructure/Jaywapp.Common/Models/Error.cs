using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Jaywapp.Common.Models
{
    /// <summary>
    /// 오류 정보를 나타내는 불변 클래스입니다.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// 오류 코드입니다.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// 오류 메시지입니다.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// 관련 예외 정보입니다.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// 추가 메타데이터입니다.
        /// </summary>
        public IReadOnlyDictionary<string, object> Metadata { get; }

        /// <summary>
        /// <see cref="Error"/>의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="code">오류 코드입니다.</param>
        /// <param name="message">오류 메시지입니다.</param>
        /// <param name="exception">관련 예외입니다.</param>
        /// <param name="metadata">추가 메타데이터입니다.</param>
        public Error(string code, string message, Exception exception = null, IDictionary<string, object> metadata = null)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Exception = exception;
            Metadata = metadata != null
                ? new ReadOnlyDictionary<string, object>(new Dictionary<string, object>(metadata))
                : new ReadOnlyDictionary<string, object>(new Dictionary<string, object>());
        }

        /// <summary>
        /// 간단한 오류를 생성합니다.
        /// </summary>
        /// <param name="code">오류 코드입니다.</param>
        /// <param name="message">오류 메시지입니다.</param>
        /// <returns>새 <see cref="Error"/> 인스턴스입니다.</returns>
        public static Error Create(string code, string message)
        {
            return new Error(code, message);
        }

        /// <summary>
        /// 오류 정보를 문자열로 반환합니다.
        /// </summary>
        /// <returns>"{Code}: {Message}" 형식의 문자열입니다.</returns>
        public override string ToString()
        {
            return $"{Code}: {Message}";
        }
    }
}
