using Jaywapp.Infrastructure.Helpers;
using System.Xml.Linq;

namespace Jaywapp.Infrastructure.Tests.Helpers
{
    [TestFixture]
    public class TestXmlHelper
    {
        private XElement _testElement;

        [SetUp]
        public void Setup()
        {
            _testElement = new XElement("root",
                new XAttribute("id", "123"),
                new XAttribute("name", " TestName "),
                new XAttribute("empty", ""));
        }

        [Test]
        public void TestGetAttributeValue_ExistingAttribute_ReturnsValue()
        {
            // Act
            var result = _testElement.GetAttributeValue("name");

            // Assert
            Assert.That(result, Is.EqualTo("TestName"));
        }

        [Test]
        public void TestGetAttributeValue_NonExistingAttribute_ReturnsDefaultValue()
        {
            // Act
            var result = _testElement.GetAttributeValue("age", "30");

            // Assert
            Assert.That(result, Is.EqualTo("30"));
        }

        [Test]
        public void TestGetAttributeValue_NonExistingAttribute_ReturnsNull()
        {
            // Act
            var result = _testElement.GetAttributeValue("age");

            // Assert
            Assert.That(result, Is.Null);
        }

        // ---

        [Test]
        public void TestTryGetAttributeValue_ExistingAttribute_ReturnsTrueAndValue()
        {
            // Act
            var success = _testElement.TryGetAttributeValue("id", out string result);

            // Assert
            Assert.That(success, Is.True);
            Assert.That(result, Is.EqualTo("123"));
        }

        [Test]
        public void TestTryGetAttributeValue_NonExistingAttribute_ReturnsFalseAndNull()
        {
            // Act
            var success = _testElement.TryGetAttributeValue("nonExistent", out string result);

            // Assert
            Assert.That(success, Is.False);
            Assert.That(result, Is.Null);
        }

        // ---

        [Test]
        public void TestGetAttributeValueOrEmpty_ExistingAttribute_ReturnsValue()
        {
            // Act
            var result = _testElement.GetAttributeValueOrEmpty("name");

            // Assert
            Assert.That(result, Is.EqualTo("TestName"));
        }

        [Test]
        public void TestGetAttributeValueOrEmpty_NonExistingAttribute_ReturnsEmptyString()
        {
            // Act
            var result = _testElement.GetAttributeValueOrEmpty("nonExistent");

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void TestGetAttributeValueOrEmpty_EmptyAttribute_ReturnsEmptyString()
        {
            // Act
            var result = _testElement.GetAttributeValueOrEmpty("empty");

            // Assert
            Assert.That(result, Is.Empty);
        }
    }
}
