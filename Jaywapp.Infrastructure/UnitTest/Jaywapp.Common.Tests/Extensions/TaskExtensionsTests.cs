using Jaywapp.Common.Extensions;
using CommonTaskExtensions = Jaywapp.Common.Extensions.TaskExtensions;

namespace Jaywapp.Common.Tests.Extensions
{
    [TestFixture]
    public class TaskExtensionsTests
    {
        [Test]
        public async Task WithTimeout_CompletesInTime_ReturnsResult()
        {
            var task = Task.FromResult(42);
            var result = await task.WithTimeout(TimeSpan.FromSeconds(1));
            Assert.That(result, Is.EqualTo(42));
        }

        [Test]
        public void WithTimeout_ExceedsTimeout_ThrowsTimeoutException()
        {
            var task = Task.Delay(TimeSpan.FromSeconds(10)).ContinueWith(_ => 42);
            Assert.ThrowsAsync<TimeoutException>(async () =>
                await task.WithTimeout(TimeSpan.FromMilliseconds(50)));
        }

        [Test]
        public async Task WithTimeout_NonGeneric_CompletesInTime()
        {
            var task = Task.CompletedTask;
            await task.WithTimeout(TimeSpan.FromSeconds(1));
            Assert.Pass();
        }

        [Test]
        public async Task WithRetry_SucceedsOnFirstTry_ReturnsResult()
        {
            var result = await CommonTaskExtensions.WithRetry(
                () => Task.FromResult(42), 3, TimeSpan.FromMilliseconds(10));
            Assert.That(result, Is.EqualTo(42));
        }

        [Test]
        public async Task WithRetry_FailsThenSucceeds_ReturnsResult()
        {
            var attempt = 0;
            var result = await CommonTaskExtensions.WithRetry(() =>
            {
                attempt++;
                if (attempt < 3)
                    throw new InvalidOperationException("fail");
                return Task.FromResult(99);
            }, 3, TimeSpan.FromMilliseconds(10));

            Assert.That(result, Is.EqualTo(99));
            Assert.That(attempt, Is.EqualTo(3));
        }

        [Test]
        public void WithRetry_AllFail_ThrowsLastException()
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await CommonTaskExtensions.WithRetry<int>(
                    () => throw new InvalidOperationException("always fail"),
                    2, TimeSpan.FromMilliseconds(10)));
        }

        [Test]
        public async Task WithRetry_NonGeneric_SucceedsOnFirstTry()
        {
            var executed = false;
            await CommonTaskExtensions.WithRetry(() =>
            {
                executed = true;
                return Task.CompletedTask;
            }, 3, TimeSpan.FromMilliseconds(10));

            Assert.That(executed, Is.True);
        }

        [Test]
        public void WithRetry_NullFactory_ThrowsArgumentNull()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await CommonTaskExtensions.WithRetry<int>(null!, 3, TimeSpan.FromMilliseconds(10)));
        }

        [Test]
        public void WithRetry_NegativeRetries_ThrowsArgumentOutOfRange()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                await CommonTaskExtensions.WithRetry(
                    () => Task.FromResult(1), -1, TimeSpan.FromMilliseconds(10)));
        }
    }
}
