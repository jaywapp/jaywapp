using System;

namespace Jaywapp.Common.Models
{
    /// <summary>
    /// 작업 결과를 나타내는 클래스입니다.
    /// 예외를 던지지 않고 성공/실패를 표현합니다.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 작업 성공 여부입니다.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// 작업 실패 여부입니다.
        /// </summary>
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// 실패 시 오류 정보입니다. 성공 시 null입니다.
        /// </summary>
        public Error Error { get; }

        /// <summary>
        /// <see cref="Result"/>의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="isSuccess">성공 여부입니다.</param>
        /// <param name="error">오류 정보입니다.</param>
        protected Result(bool isSuccess, Error error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        /// <summary>
        /// 성공 결과를 생성합니다.
        /// </summary>
        /// <returns>성공 <see cref="Result"/> 인스턴스입니다.</returns>
        public static Result Success()
        {
            return new Result(true, null);
        }

        /// <summary>
        /// 실패 결과를 생성합니다.
        /// </summary>
        /// <param name="error">오류 정보입니다.</param>
        /// <returns>실패 <see cref="Result"/> 인스턴스입니다.</returns>
        /// <exception cref="ArgumentNullException">error가 null인 경우 발생합니다.</exception>
        public static Result Failure(Error error)
        {
            if (error == null)
                throw new ArgumentNullException(nameof(error));

            return new Result(false, error);
        }

        /// <summary>
        /// 실패 결과를 생성합니다.
        /// </summary>
        /// <param name="code">오류 코드입니다.</param>
        /// <param name="message">오류 메시지입니다.</param>
        /// <returns>실패 <see cref="Result"/> 인스턴스입니다.</returns>
        public static Result Failure(string code, string message)
        {
            return Failure(new Error(code, message));
        }

        /// <summary>
        /// 값을 포함하는 성공 결과를 생성합니다.
        /// </summary>
        /// <typeparam name="T">값의 타입입니다.</typeparam>
        /// <param name="value">결과 값입니다.</param>
        /// <returns>성공 <see cref="Result{T}"/> 인스턴스입니다.</returns>
        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(value);
        }

        /// <summary>
        /// 값 타입의 실패 결과를 생성합니다.
        /// </summary>
        /// <typeparam name="T">값의 타입입니다.</typeparam>
        /// <param name="error">오류 정보입니다.</param>
        /// <returns>실패 <see cref="Result{T}"/> 인스턴스입니다.</returns>
        public static Result<T> Failure<T>(Error error)
        {
            return new Result<T>(error);
        }
    }
}
