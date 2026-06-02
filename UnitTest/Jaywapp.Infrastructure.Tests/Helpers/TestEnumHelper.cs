using System.ComponentModel;
using Jaywapp.Infrastructure.Helpers;

namespace Jaywapp.Infrastructure.Tests.Helpers
{
    public enum TestEnumWithDescription
    {
        [System.ComponentModel.Description("첫 번째")]
        First,
        [System.ComponentModel.Description("두 번째")]
        Second,
        NoDescription
    }

    [TestFixture]
    public class TestEnumHelper
    {
        [Test]
        public void GetValues_ReturnsAllEnumValues()
        {
            // Act
            var values = EnumHelper.GetValues<TestEnumWithDescription>().ToList();

            // Assert
            Assert.That(values.Count, Is.EqualTo(3));
            Assert.That(values, Does.Contain(TestEnumWithDescription.First));
            Assert.That(values, Does.Contain(TestEnumWithDescription.Second));
            Assert.That(values, Does.Contain(TestEnumWithDescription.NoDescription));
        }

        [Test]
        public void TryGetDescription_WithDescription_ReturnsTrue()
        {
            // Act
            var result = EnumHelper.TryGetDescription(TestEnumWithDescription.First, out string description);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(description, Is.EqualTo("첫 번째"));
        }

        [Test]
        public void TryGetDescription_WithoutDescription_ReturnsFalse()
        {
            // Act
            var result = EnumHelper.TryGetDescription(TestEnumWithDescription.NoDescription, out string description);

            // Assert
            Assert.That(result, Is.False);
            Assert.That(description, Is.Null);
        }

        [Test]
        public void GetDescriptionOrToString_WithDescription_ReturnsDescription()
        {
            // Act
            var result = TestEnumWithDescription.First.GetDescriptionOrToString();

            // Assert
            Assert.That(result, Is.EqualTo("첫 번째"));
        }

        [Test]
        public void GetDescriptionOrToString_WithoutDescription_ReturnsToString()
        {
            // Act
            var result = TestEnumWithDescription.NoDescription.GetDescriptionOrToString();

            // Assert
            Assert.That(result, Is.EqualTo("NoDescription"));
        }

        [Test]
        public void GetValueFromDescription_ValidDescription_ReturnsValue()
        {
            // Act
            var result = "첫 번째".GetValueFromDescription<TestEnumWithDescription>();

            // Assert
            Assert.That(result, Is.EqualTo(TestEnumWithDescription.First));
        }

        [Test]
        public void GetValueFromDescription_InvalidDescription_ReturnsDefault()
        {
            // Act
            var result = "없는값".GetValueFromDescription<TestEnumWithDescription>();

            // Assert
            Assert.That(result, Is.EqualTo(default(TestEnumWithDescription)));
        }

        [Test]
        public void TryParseValueFromDescription_ValidName_ReturnsTrue()
        {
            // Act
            var result = EnumHelper.TryParseValueFromDescription<TestEnumWithDescription>("NoDescription", out var value);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(value, Is.EqualTo(TestEnumWithDescription.NoDescription));
        }
    }
}
