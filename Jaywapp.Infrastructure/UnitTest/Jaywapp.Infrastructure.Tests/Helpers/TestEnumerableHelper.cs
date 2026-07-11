using System.Collections.ObjectModel;
using Jaywapp.Infrastructure.Helpers;

namespace Jaywapp.Infrastructure.Tests.Helpers
{
    [TestFixture]
    public class TestEnumerableHelper
    {
        [Test]
        public void TestIsNullOrEmpty_NullCollection_ReturnsTrue()
        {
            // Act
            var result = EnumerableHelper.IsNullOrEmpty<int>(null);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void TestIsNullOrEmpty_EmptyCollection_ReturnsTrue()
        {
            // Act
            var result = EnumerableHelper.IsNullOrEmpty(Enumerable.Empty<string>());

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void TestIsNullOrEmpty_PopulatedCollection_ReturnsFalse()
        {
            // Act
            var result = EnumerableHelper.IsNullOrEmpty(new List<int> { 1, 2, 3 });

            // Assert
            Assert.That(result, Is.False);
        }

        // ---

        [Test]
        public void TestToObservableCollection_ConvertsToList()
        {
            // Arrange
            var list = new List<string> { "a", "b", "c" };

            // Act
            var collection = list.ToObservableCollection();

            // Assert
            Assert.That(collection, Is.TypeOf<ObservableCollection<string>>());
            Assert.That(collection.Count, Is.EqualTo(3));
            Assert.That(collection, Is.EqualTo(list));
        }

        // ---

        [Test]
        public void TestChainPairing_NonCircular_ReturnsCorrectPairs()
        {
            // Arrange
            var items = new List<int> { 1, 2, 3, 4 };

            // Act
            var pairs = items.ChainPairing().ToList();

            // Assert
            Assert.That(pairs.Count, Is.EqualTo(3));
            Assert.That(pairs[0], Is.EqualTo((1, 2)));
            Assert.That(pairs[1], Is.EqualTo((2, 3)));
            Assert.That(pairs[2], Is.EqualTo((3, 4)));
        }

        [Test]
        public void TestChainPairing_Circular_ReturnsCorrectPairs()
        {
            // Arrange
            var items = new List<int> { 1, 2, 3 };

            // Act
            var pairs = items.ChainPairing(isCircular: true).ToList();

            // Assert
            Assert.That(pairs.Count, Is.EqualTo(3));
            Assert.That(pairs[0], Is.EqualTo((1, 2)));
            Assert.That(pairs[1], Is.EqualTo((2, 3)));
            Assert.That(pairs[2], Is.EqualTo((3, 1)));
        }

        [Test]
        public void TestChainPairing_SingleItem_ReturnsEmpty()
        {
            // Arrange
            var items = new List<int> { 1 };

            // Act
            var pairs = items.ChainPairing().ToList();

            // Assert
            Assert.That(pairs, Is.Empty);
        }

        [Test]
        public void TestChainPairing_EmptyList_ReturnsEmpty()
        {
            // Arrange
            var items = new List<int>();

            // Act
            var pairs = items.ChainPairing().ToList();

            // Assert
            Assert.That(pairs, Is.Empty);
        }
    }
}
