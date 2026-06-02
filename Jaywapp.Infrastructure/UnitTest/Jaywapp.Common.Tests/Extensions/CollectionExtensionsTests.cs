using Jaywapp.Common.Extensions;

namespace Jaywapp.Common.Tests.Extensions
{
    [TestFixture]
    public class CollectionExtensionsTests
    {
        [Test]
        public void IsEmpty_EmptyCollection_ReturnsTrue()
        {
            Assert.That(Array.Empty<int>().IsEmpty(), Is.True);
        }

        [Test]
        public void IsEmpty_NonEmptyCollection_ReturnsFalse()
        {
            Assert.That(new[] { 1 }.IsEmpty(), Is.False);
        }

        [Test]
        public void IsEmpty_Null_ThrowsArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => ((IEnumerable<int>)null!).IsEmpty());
        }

        [Test]
        public void None_EmptyCollection_ReturnsTrue()
        {
            Assert.That(Array.Empty<int>().None(), Is.True);
        }

        [Test]
        public void None_WithPredicate_NoMatch_ReturnsTrue()
        {
            Assert.That(new[] { 1, 2, 3 }.None(x => x > 10), Is.True);
        }

        [Test]
        public void None_WithPredicate_HasMatch_ReturnsFalse()
        {
            Assert.That(new[] { 1, 2, 3 }.None(x => x == 2), Is.False);
        }

        [Test]
        public void SafeForEach_NullSource_DoesNothing()
        {
            var count = 0;
            ((IEnumerable<int>?)null).SafeForEach(x => count++);
            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public void SafeForEach_ValidSource_ExecutesAction()
        {
            var result = new List<int>();
            new[] { 1, 2, 3 }.SafeForEach(x => result.Add(x * 2));
            Assert.That(result, Is.EqualTo(new[] { 2, 4, 6 }));
        }

        [Test]
        public void SafeForEach_NullAction_ThrowsArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => new[] { 1 }.SafeForEach(null!));
        }

        [Test]
        public void Batch_SplitsCorrectly()
        {
            var items = new[] { 1, 2, 3, 4, 5 };
            var batches = items.Batch(2).Select(b => b.ToList()).ToList();

            Assert.That(batches.Count, Is.EqualTo(3));
            Assert.That(batches[0], Is.EqualTo(new[] { 1, 2 }));
            Assert.That(batches[1], Is.EqualTo(new[] { 3, 4 }));
            Assert.That(batches[2], Is.EqualTo(new[] { 5 }));
        }

        [Test]
        public void Batch_EmptyCollection_ReturnsEmpty()
        {
            var batches = Array.Empty<int>().Batch(3).ToList();
            Assert.That(batches, Is.Empty);
        }

        [Test]
        public void Batch_InvalidSize_ThrowsArgumentOutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new[] { 1 }.Batch(0).ToList());
        }

        [Test]
        public void DistinctBy_RemovesDuplicatesByKey()
        {
            var items = new[] { "apple", "apricot", "banana", "blueberry" };
            var result = Jaywapp.Common.Extensions.CollectionExtensions.DistinctBy(items, x => x[0]).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0], Is.EqualTo("apple"));
            Assert.That(result[1], Is.EqualTo("banana"));
        }

        [Test]
        public void DistinctBy_Null_ThrowsArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Jaywapp.Common.Extensions.CollectionExtensions.DistinctBy<int, int>(null!, x => x).ToList());
        }
    }
}
