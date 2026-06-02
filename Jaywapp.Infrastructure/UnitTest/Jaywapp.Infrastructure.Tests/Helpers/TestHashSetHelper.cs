using Jaywapp.Infrastructure.Helpers;

namespace Jaywapp.Infrastructure.Tests.Helpers
{
    [TestFixture]
    public class TestHashSetHelper
    {
        [Test]
        public void TestAddRange()
        {
            // Arrange
            var hashSet = new HashSet<int> { 1, 2, 3 };
            var targets = new List<int> { 3, 4, 5 };

            // Act
            hashSet.AddRange(targets);

            // Assert
            Assert.That(hashSet.Count, Is.EqualTo(5));
            Assert.That(hashSet, Does.Contain(1));
            Assert.That(hashSet, Does.Contain(2));
            Assert.That(hashSet, Does.Contain(3));
            Assert.That(hashSet, Does.Contain(4));
            Assert.That(hashSet, Does.Contain(5));
        }

        [Test]
        public void TestRemoveRange()
        {
            // Arrange
            var hashSet = new HashSet<string> { "a", "b", "c", "d" };
            var targets = new List<string> { "c", "d", "e" };

            // Act
            hashSet.RemoveRange(targets);

            // Assert
            Assert.That(hashSet.Count, Is.EqualTo(2));
            Assert.That(hashSet, Does.Contain("a"));
            Assert.That(hashSet, Does.Contain("b"));
            Assert.That(hashSet, Does.Not.Contain("c"));
            Assert.That(hashSet, Does.Not.Contain("d"));
        }
    }
}
