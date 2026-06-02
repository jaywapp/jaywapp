using Jaywapp.Infrastructure.Interfaces;
using Jaywapp.Infrastructure.Models;

namespace Jaywapp.Infrastructure.Tests.Models
{
    [TestFixture]
    public class TestFilterGroup
    {
        [Test]
        public void IsFiltered_AndGroup_AllMatch_ReturnsTrue()
        {
            // Arrange
            var group = new FilterGroup
            {
                Logical = eLogicalOperator.AND,
                Children = new List<IFilter>
                {
                    new Filter
                    {
                        Logical = eLogicalOperator.AND,
                        Selector = new SimplePropertySelector("Value", eFilteringType.String, o => (string)o),
                        Operator = eFilteringOperator.Contains,
                        Expect = "Hello"
                    },
                    new Filter
                    {
                        Logical = eLogicalOperator.AND,
                        Selector = new SimplePropertySelector("Value", eFilteringType.String, o => (string)o),
                        Operator = eFilteringOperator.EndsWith,
                        Expect = "World"
                    }
                }
            };

            // Act
            var result = group.IsFiltered("Hello World");

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsFiltered_AndGroup_NotAllMatch_ReturnsFalse()
        {
            // Arrange
            var group = new FilterGroup
            {
                Logical = eLogicalOperator.AND,
                Children = new List<IFilter>
                {
                    new Filter
                    {
                        Logical = eLogicalOperator.AND,
                        Selector = new SimplePropertySelector("Value", eFilteringType.String, o => (string)o),
                        Operator = eFilteringOperator.Contains,
                        Expect = "Hello"
                    },
                    new Filter
                    {
                        Logical = eLogicalOperator.AND,
                        Selector = new SimplePropertySelector("Value", eFilteringType.String, o => (string)o),
                        Operator = eFilteringOperator.Contains,
                        Expect = "Foo"
                    }
                }
            };

            // Act
            var result = group.IsFiltered("Hello World");

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsFiltered_OrGroup_OneMatch_ReturnsTrue()
        {
            // Arrange
            var group = new FilterGroup
            {
                Logical = eLogicalOperator.OR,
                Children = new List<IFilter>
                {
                    new Filter
                    {
                        Logical = eLogicalOperator.OR,
                        Selector = new SimplePropertySelector("Value", eFilteringType.String, o => (string)o),
                        Operator = eFilteringOperator.Equal,
                        Expect = "Foo"
                    },
                    new Filter
                    {
                        Logical = eLogicalOperator.OR,
                        Selector = new SimplePropertySelector("Value", eFilteringType.String, o => (string)o),
                        Operator = eFilteringOperator.Equal,
                        Expect = "Hello"
                    }
                }
            };

            // Act
            var result = group.IsFiltered("Hello");

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsFiltered_EmptyFilters_ReturnsFalse()
        {
            // Arrange
            var group = new FilterGroup
            {
                Logical = eLogicalOperator.AND,
                Children = new List<IFilter>()
            };

            // Act
            var result = group.IsFiltered("Hello");

            // Assert
            Assert.That(result, Is.False);
        }
    }
}
