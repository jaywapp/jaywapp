using System;

namespace Jaywapp.Common.Models
{
    /// <summary>
    /// 값을 포함하는 작업 결과를 나타내는 클래스입니다.
    /// </summary>
    /// <typeparam name="T">결과 값의 타입입니다.</typeparam>
    public class Result<T> : Result
    {
        private readonly T _value;

        /// <summary>
        /// 결과 값입니다. 실패 상태에서 접근하면 예외가 발생합니다.
        /// </summary>
        /// <exception cref="InvalidOperationException">실패 상태에서 접근한 경우 발생합니다.</exception>
        public T Value
        {
            get
            {
                if (IsFailure)
                    throw new InvalidOperationException("실패 결과에서 값에 접근할 수 없습니다.");

                return _value;
            }
        }

        /// <summary>
        /// 성공 결과를 초기화합니다.
        /// </summary>
        /// <param name="value">결과 값입니다.</param>
        internal Result(T value) : base(true, null)
        {
            _value = value;
        }

        /// <summary>
        /// 실패 결과를 초기화합니다.
        /// </summary>
        /// <param name="error">오류 정보입니다.</param>
        internal Result(Error error) : base(false, error)
        {
            _value = default(T);
        }
    }
}
