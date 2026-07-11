using Jaywapp.Common.Models;

namespace Jaywapp.Common.Tests.Models
{
    [TestFixture]
    public class RangeTests
    {
        [Test]
        public void Range_ValidMinMax_CreatesInstance()
        {
            var range = new Range<int>(1, 10);
            Assert.That(range.Min, Is.EqualTo(1));
            Assert.That(range.Max, Is.EqualTo(10));
        }

        [Test]
        public void Range_MinGreaterThanMax_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Range<int>(10, 1));
        }

        [Test]
        public void Range_Contains_InRange_ReturnsTrue()
        {
            var range = new Range<int>(1, 10);
            Assert.That(range.Contains(5), Is.True);
            Assert.That(range.Contains(1), Is.True);
            Assert.That(range.Contains(10), Is.True);
        }

        [Test]
        public void Range_Contains_OutOfRange_ReturnsFalse()
        {
            var range = new Range<int>(1, 10);
            Assert.That(range.Contains(0), Is.False);
            Assert.That(range.Contains(11), Is.False);
        }

        [Test]
        public void Range_Overlaps_Overlapping_ReturnsTrue()
        {
            var range1 = new Range<int>(1, 10);
            var range2 = new Range<int>(5, 15);
            Assert.That(range1.Overlaps(range2), Is.True);
        }

        [Test]
        public void Range_Overlaps_NonOverlapping_ReturnsFalse()
        {
            var range1 = new Range<int>(1, 5);
            var range2 = new Range<int>(6, 10);
            Assert.That(range1.Overlaps(range2), Is.False);
        }

        [Test]
        public void Range_Overlaps_Adjacent_ReturnsTrue()
        {
            var range1 = new Range<int>(1, 5);
            var range2 = new Range<int>(5, 10);
            Assert.That(range1.Overlaps(range2), Is.True);
        }

        [Test]
        public void DatePeriod_ValidPeriod_CreatesInstance()
        {
            var start = new DateTime(2024, 1, 1);
            var end = new DateTime(2024, 12, 31);
            var period = new DatePeriod(start, end);

            Assert.That(period.Start, Is.EqualTo(start));
            Assert.That(period.End, Is.EqualTo(end));
            Assert.That(period.IsStartInclusive, Is.True);
            Assert.That(period.IsEndInclusive, Is.True);
            Assert.That(period.Duration, Is.EqualTo(end - start));
        }

        [Test]
        public void DatePeriod_StartAfterEnd_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new DatePeriod(new DateTime(2024, 12, 31), new DateTime(2024, 1, 1)));
        }

        [Test]
        public void DatePeriod_Contains_Inclusive_ReturnsTrue()
        {
            var period = new DatePeriod(
                new DateTime(2024, 1, 1),
                new DateTime(2024, 12, 31));

            Assert.That(period.Contains(new DateTime(2024, 6, 15)), Is.True);
            Assert.That(period.Contains(new DateTime(2024, 1, 1)), Is.True);
            Assert.That(period.Contains(new DateTime(2024, 12, 31)), Is.True);
        }

        [Test]
        public void DatePeriod_Contains_Exclusive_ReturnsFalseOnBoundary()
        {
            var period = new DatePeriod(
                new DateTime(2024, 1, 1),
                new DateTime(2024, 12, 31),
                isStartInclusive: false,
                isEndInclusive: false);

            Assert.That(period.Contains(new DateTime(2024, 1, 1)), Is.False);
            Assert.That(period.Contains(new DateTime(2024, 12, 31)), Is.False);
            Assert.That(period.Contains(new DateTime(2024, 6, 15)), Is.True);
        }
    }
}
