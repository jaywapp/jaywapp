using Jaywapp.Common.Extensions;

namespace Jaywapp.Common.Tests.Extensions
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [Test]
        public void StartOfDay_ReturnsDateOnly()
        {
            var dt = new DateTime(2024, 6, 15, 14, 30, 45);
            var result = dt.StartOfDay();

            Assert.That(result, Is.EqualTo(new DateTime(2024, 6, 15, 0, 0, 0)));
        }

        [Test]
        public void EndOfDay_ReturnsLastTickOfDay()
        {
            var dt = new DateTime(2024, 6, 15, 14, 30, 45);
            var result = dt.EndOfDay();

            Assert.That(result.Year, Is.EqualTo(2024));
            Assert.That(result.Month, Is.EqualTo(6));
            Assert.That(result.Day, Is.EqualTo(15));
            Assert.That(result.Hour, Is.EqualTo(23));
            Assert.That(result.Minute, Is.EqualTo(59));
            Assert.That(result.Second, Is.EqualTo(59));
        }

        [Test]
        public void IsInRange_InRange_ReturnsTrue()
        {
            var dt = new DateTime(2024, 6, 15);
            var start = new DateTime(2024, 6, 1);
            var end = new DateTime(2024, 6, 30);

            Assert.That(dt.IsInRange(start, end), Is.True);
        }

        [Test]
        public void IsInRange_OnBoundary_ReturnsTrue()
        {
            var dt = new DateTime(2024, 6, 1);
            var start = new DateTime(2024, 6, 1);
            var end = new DateTime(2024, 6, 30);

            Assert.That(dt.IsInRange(start, end), Is.True);
        }

        [Test]
        public void IsInRange_OutOfRange_ReturnsFalse()
        {
            var dt = new DateTime(2024, 7, 1);
            var start = new DateTime(2024, 6, 1);
            var end = new DateTime(2024, 6, 30);

            Assert.That(dt.IsInRange(start, end), Is.False);
        }

        [Test]
        public void ToUtcSafe_Utc_ReturnsSame()
        {
            var dt = new DateTime(2024, 6, 15, 12, 0, 0, DateTimeKind.Utc);
            var result = dt.ToUtcSafe();

            Assert.That(result.Kind, Is.EqualTo(DateTimeKind.Utc));
            Assert.That(result, Is.EqualTo(dt));
        }

        [Test]
        public void ToUtcSafe_Unspecified_SpecifiesAsUtc()
        {
            var dt = new DateTime(2024, 6, 15, 12, 0, 0, DateTimeKind.Unspecified);
            var result = dt.ToUtcSafe();

            Assert.That(result.Kind, Is.EqualTo(DateTimeKind.Utc));
            Assert.That(result.Hour, Is.EqualTo(12));
        }

        [Test]
        public void ToLocalSafe_Local_ReturnsSame()
        {
            var dt = new DateTime(2024, 6, 15, 12, 0, 0, DateTimeKind.Local);
            var result = dt.ToLocalSafe();

            Assert.That(result.Kind, Is.EqualTo(DateTimeKind.Local));
            Assert.That(result, Is.EqualTo(dt));
        }

        [Test]
        public void ToLocalSafe_Unspecified_SpecifiesAsLocal()
        {
            var dt = new DateTime(2024, 6, 15, 12, 0, 0, DateTimeKind.Unspecified);
            var result = dt.ToLocalSafe();

            Assert.That(result.Kind, Is.EqualTo(DateTimeKind.Local));
            Assert.That(result.Hour, Is.EqualTo(12));
        }
    }
}
