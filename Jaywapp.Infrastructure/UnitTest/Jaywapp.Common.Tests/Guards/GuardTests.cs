using Jaywapp.Common.Guards;

namespace Jaywapp.Common.Tests.Guards
{
    [TestFixture]
    public class GuardTests
    {
        [Test]
        public void NotNull_ValidValue_ReturnsValue()
        {
            var result = Guard.NotNull("hello", "param");
            Assert.That(result, Is.EqualTo("hello"));
        }

        [Test]
        public void NotNull_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.NotNull<string>(null!, "param"));
        }

        [Test]
        public void NotNullOrEmpty_ValidString_ReturnsString()
        {
            var result = Guard.NotNullOrEmpty("hello", "param");
            Assert.That(result, Is.EqualTo("hello"));
        }

        [Test]
        public void NotNullOrEmpty_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.NotNullOrEmpty(null!, "param"));
        }

        [Test]
        public void NotNullOrEmpty_Empty_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Guard.NotNullOrEmpty("", "param"));
        }

        [Test]
        public void NotNullOrWhiteSpace_ValidString_ReturnsString()
        {
            var result = Guard.NotNullOrWhiteSpace("hello", "param");
            Assert.That(result, Is.EqualTo("hello"));
        }

        [Test]
        public void NotNullOrWhiteSpace_Whitespace_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Guard.NotNullOrWhiteSpace("   ", "param"));
        }

        [Test]
        public void InRange_ValidValue_ReturnsValue()
        {
            var result = Guard.InRange(5, 1, 10, "param");
            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void InRange_OnBoundary_ReturnsValue()
        {
            Assert.That(Guard.InRange(1, 1, 10, "param"), Is.EqualTo(1));
            Assert.That(Guard.InRange(10, 1, 10, "param"), Is.EqualTo(10));
        }

        [Test]
        public void InRange_OutOfRange_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.InRange(0, 1, 10, "param"));
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.InRange(11, 1, 10, "param"));
        }

        [Test]
        public void NotEmpty_ValidCollection_ReturnsCollection()
        {
            var items = new[] { 1, 2, 3 };
            var result = Guard.NotEmpty(items, "param");
            Assert.That(result, Is.EqualTo(items));
        }

        [Test]
        public void NotEmpty_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.NotEmpty<int>(null!, "param"));
        }

        [Test]
        public void NotEmpty_Empty_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Guard.NotEmpty(Array.Empty<int>(), "param"));
        }

        [Test]
        public void Requires_True_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Guard.Requires(true, "ok"));
        }

        [Test]
        public void Requires_False_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => Guard.Requires(false, "조건 불충족"));
        }
    }
}
