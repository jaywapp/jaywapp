using System.Collections.ObjectModel;
using Jaywapp.Infrastructure.Helpers;

namespace Jaywapp.Infrastructure.Tests
{
    [TestFixture]
    public class TestCollectionHelper
    {
        [Test]
        public void TestAddRange_List_AddsItemsToList()
        {
            // Arrange
            var list = new List<int> { 1, 2, 3 };
            var itemsToAdd = new[] { 4, 5, 6 };

            // Act
            list.AddRange(itemsToAdd);

            // Assert
            Assert.That(list.Count, Is.EqualTo(6));
            Assert.That(list, Is.EqualTo(new[] { 1, 2, 3, 4, 5, 6 }));
        }

        [Test]
        public void TestAddRange_ObservableCollection_AddsItemsToCollection()
        {
            // Arrange
            var collection = new ObservableCollection<string> { "a", "b" };
            var itemsToAdd = new[] { "c", "d" };

            // Act
            collection.AddRange(itemsToAdd);

            // Assert
            Assert.That(collection.Count, Is.EqualTo(4));
            Assert.That(collection, Is.EqualTo(new[] { "a", "b", "c", "d" }));
        }

        [Test]
        public void TestAddRange_EmptySource_DoesNothing()
        {
            // Arrange
            var list = new List<int> { 1, 2, 3 };
            var itemsToAdd = new int[0];
            var originalCount = list.Count;

            // Act
            list.AddRange(itemsToAdd);

            // Assert
            Assert.That(list.Count, Is.EqualTo(originalCount));
            Assert.That(list, Is.EqualTo(new[] { 1, 2, 3 }));
        }
    }
}
