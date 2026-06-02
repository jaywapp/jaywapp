using Jaywapp.Infrastructure.Interfaces;
using Jaywapp.Infrastructure.Models;

namespace Jaywapp.Infrastructure.Tests.Models
{
    public class SimplePropertySelector : IFilterPropertySelector
    {
        public string Name { get; }
        public eFilteringType Type { get; }
        private readonly Func<object, object> _selector;

        public SimplePropertySelector(string name, eFilteringType type, Func<object, object> selector)
        {
            Name = name;
            Type = type;
            _selector = selector;
        }

        public object Select(object target) => _selector(target);
    }

    [TestFixture]
    public class TestFilter
    {
        [Test]
        public void IsFiltered_StringEqual_ReturnsTrue()
        {
            // Arrange
            var filter = new Filter
            {
                Selector = new SimplePropertySelector("Name", eFilteringType.String, o => (string)o),
                Operator = eFilteringOperator.Equal,
                Expect = "Hello"
            };

            // Act
            var result = filter.IsFiltered("Hello");

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsFiltered_StringNotEqual_ReturnsFalse()
        {
            // Arrange
            var filter = new Filter
            {
                Selector = new SimplePropertySelector("Name", eFilteringType.String, o => (string)o),
                Operator = eFilteringOperator.Equal,
                Expect = "World"
            };

            // Act
            var result = filter.IsFiltered("Hello");

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsFiltered_StringContains_ReturnsTrue()
        {
            // Arrange
            var filter = new Filter
            {
                Selector = new SimplePropertySelector("Name", eFilteringType.String, o => (string)o),
                Operator = eFilteringOperator.Contains,
                Expect = "ell"
            };

            // Act
            var result = filter.IsFiltered("Hello");

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsFiltered_StringStartsWith_ReturnsTrue()
        {
            // Arrange
            var filter = new Filter
            {
                Selector = new SimplePropertySelector("Name", eFilteringType.String, o => (string)o),
                Operator = eFilteringOperator.StartsWith,
                Expect = "Hel"
            };

            // Act
            var result = filter.IsFiltered("Hello");

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsFiltered_NumberLessThan_ReturnsTrue()
        {
            // Arrange
            var filter = new Filter
            {
                Selector = new SimplePropertySelector("Value", eFilteringType.Number, o => (int)o),
                Operator = eFilteringOperator.LessThan,
                Expect = "10"
            };

            // Act
            var result = filter.IsFiltered(5);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsFiltered_NumberGreaterEqual_ReturnsTrue()
        {
            // Arrange
            var filter = new Filter
            {
                Selector = new SimplePropertySelector("Value", eFilteringType.Number, o => (int)o),
                Operator = eFilteringOperator.GreaterEqual,
                Expect = "5"
            };

            // Act
            var result = filter.IsFiltered(5);

            // Assert
            Assert.That(result, Is.True);
        }
    }
}
