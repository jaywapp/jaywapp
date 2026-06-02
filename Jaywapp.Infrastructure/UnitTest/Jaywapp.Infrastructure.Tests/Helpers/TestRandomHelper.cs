using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jaywapp.Infrastructure.Helpers;

namespace Jaywapp.Infrastructure.Tests.Helpers
{
    [TestFixture]
    public class TestRandomHelper
    {
        private Random _random;

        [SetUp]
        public void Setup()
        {
            _random = new Random();
        }

        // ---

        [Test]
        public void TestNextBoolean()
        {
            // Act
            var result = _random.NextBoolean();

            // Assert
            Assert.That(result, Is.TypeOf<bool>());
        }

        // ---

        [TestCase(10)]
        [TestCase(50)]
        [TestCase(100)]
        public void TestNextString(int maxLength)
        {
            // Act
            var result = _random.NextString(maxLength);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<string>());
            Assert.That(result.Length, Is.GreaterThan(0));
            Assert.That(result.Length, Is.LessThanOrEqualTo(maxLength));
        }

        // ---

        [Test]
        public void TestNextCharacter()
        {
            // Act
            var result = _random.NextCharacter();

            // Assert
            Assert.That(result, Is.TypeOf<char>());
            Assert.That((int)result, Is.GreaterThanOrEqualTo(33));
            Assert.That((int)result, Is.LessThanOrEqualTo(126));
        }

        // ---

        private enum TestEnum { Value1, Value2, Value3 }

        [Test]
        public void TestNextTEnum()
        {
            // Act
            var result = _random.Next<TestEnum>();

            // Assert
            var expectedValues = Enum.GetValues(typeof(TestEnum)).Cast<TestEnum>();
            Assert.That(expectedValues, Does.Contain(result));
        }

        // ---

        [Test]
        public void TestNextColor()
        {
            // Act
            var result = _random.NextColor();

            // Assert
            Assert.That(result, Is.TypeOf<Color>());
        }

        // ---

        [Test]
        public void TestNextPoint()
        {
            // Act
            var result = _random.NextPoint();

            // Assert
            Assert.That(result, Is.TypeOf<Point>());
        }

        // ---

        [Test]
        public void TestNextTSelectors()
        {
            // Arrange
            Func<int> selector1 = () => 10;
            Func<int> selector2 = () => 20;

            // Act
            var result = _random.Next(selector1, selector2);

            // Assert
            Assert.That(new[] { 10, 20 }, Does.Contain(result));
        }
    }
}
