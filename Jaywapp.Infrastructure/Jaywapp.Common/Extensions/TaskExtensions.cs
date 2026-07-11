using System;
using System.Threading.Tasks;

namespace Jaywapp.Common.Extensions
{
    /// <summary>
    /// Task 및 비동기 작업 관련 확장 메서드를 제공합니다.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Task를 안전하게 비동기로 실행합니다(fire-and-forget).
        /// 예외 발생 시 onError 콜백으로 전달합니다.
        /// </summary>
        /// <param name="task">실행할 Task입니다.</param>
        /// <param name="onError">예외 발생 시 호출되는 콜백입니다.</param>
        public static async void SafeFireAndForget(this Task task, Action<Exception> onError = null)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
            }
        }

        /// <summary>
        /// Task에 제한 시간을 적용합니다.
        /// </summary>
        /// <typeparam name="T">결과 타입입니다.</typeparam>
        /// <param name="task">대상 Task입니다.</param>
        /// <param name="timeout">제한 시간입니다.</param>
        /// <returns>Task의 결과를 반환합니다.</returns>
        /// <exception cref="TimeoutException">제한 시간을 초과한 경우 발생합니다.</exception>
        public static async Task<T> WithTimeout<T>(this Task<T> task, TimeSpan timeout)
        {
            var delayTask = Task.Delay(timeout);
            var completedTask = await Task.WhenAny(task, delayTask).ConfigureAwait(false);

            if (completedTask == delayTask)
                throw new TimeoutException($"작업이 제한 시간 {timeout}을 초과했습니다.");

            return await task.ConfigureAwait(false);
        }

        /// <summary>
        /// Task에 제한 시간을 적용합니다.
        /// </summary>
        /// <param name="task">대상 Task입니다.</param>
        /// <param name="timeout">제한 시간입니다.</param>
        /// <exception cref="TimeoutException">제한 시간을 초과한 경우 발생합니다.</exception>
        public static async Task WithTimeout(this Task task, TimeSpan timeout)
        {
            var delayTask = Task.Delay(timeout);
            var completedTask = await Task.WhenAny(task, delayTask).ConfigureAwait(false);

            if (completedTask == delayTask)
                throw new TimeoutException($"작업이 제한 시간 {timeout}을 초과했습니다.");

            await task.ConfigureAwait(false);
        }

        /// <summary>
        /// 실패 시 지정된 횟수만큼 재시도합니다.
        /// </summary>
        /// <typeparam name="T">결과 타입입니다.</typeparam>
        /// <param name="factory">실행할 비동기 작업 팩토리입니다.</param>
        /// <param name="maxRetries">최대 재시도 횟수입니다.</param>
        /// <param name="delay">재시도 간 대기 시간입니다.</param>
        /// <returns>Task의 결과를 반환합니다.</returns>
        /// <exception cref="ArgumentNullException">factory가 null인 경우 발생합니다.</exception>
        /// <exception cref="ArgumentOutOfRangeException">maxRetries가 0 미만인 경우 발생합니다.</exception>
        public static async Task<T> WithRetry<T>(Func<Task<T>> factory, int maxRetries, TimeSpan delay)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));
            if (maxRetries < 0)
                throw new ArgumentOutOfRangeException(nameof(maxRetries), "재시도 횟수는 0 이상이어야 합니다.");

            Exception lastException = null;

            for (int i = 0; i <= maxRetries; i++)
            {
                try
                {
                    return await factory().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    lastException = ex;

                    if (i < maxRetries)
                    {
                        await Task.Delay(delay).ConfigureAwait(false);
                    }
                }
            }

            throw lastException;
        }

        /// <summary>
        /// 실패 시 지정된 횟수만큼 재시도합니다.
        /// </summary>
        /// <param name="factory">실행할 비동기 작업 팩토리입니다.</param>
        /// <param name="maxRetries">최대 재시도 횟수입니다.</param>
        /// <param name="delay">재시도 간 대기 시간입니다.</param>
        /// <exception cref="ArgumentNullException">factory가 null인 경우 발생합니다.</exception>
        /// <exception cref="ArgumentOutOfRangeException">maxRetries가 0 미만인 경우 발생합니다.</exception>
        public static async Task WithRetry(Func<Task> factory, int maxRetries, TimeSpan delay)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));
            if (maxRetries < 0)
                throw new ArgumentOutOfRangeException(nameof(maxRetries), "재시도 횟수는 0 이상이어야 합니다.");

            Exception lastException = null;

            for (int i = 0; i <= maxRetries; i++)
            {
                try
                {
                    await factory().ConfigureAwait(false);
                    return;
                }
                catch (Exception ex)
                {
                    lastException = ex;

                    if (i < maxRetries)
                    {
                        await Task.Delay(delay).ConfigureAwait(false);
                    }
                }
            }

            throw lastException;
        }
    }
}
