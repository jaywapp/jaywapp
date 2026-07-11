using System.ComponentModel;
using Jaywapp.Common.Extensions;

namespace Jaywapp.Common.Tests.Extensions
{
    public enum SampleEnum
    {
        [System.ComponentModel.Description("설명 있음")]
        WithDescription,
        NoDescription,
        [System.ComponentModel.Description("다른 설명")]
        Another
    }

    [TestFixture]
    public class EnumExtensionsTests
    {
        [Test]
        public void GetDescription_WithAttribute_ReturnsDescription()
        {
            var result = SampleEnum.WithDescription.GetDescription();
            Assert.That(result, Is.EqualTo("설명 있음"));
        }

        [Test]
        public void GetDescription_WithoutAttribute_ReturnsToString()
        {
            var result = SampleEnum.NoDescription.GetDescription();
            Assert.That(result, Is.EqualTo("NoDescription"));
        }

        [Test]
        public void SafeParse_ValidName_ReturnsValue()
        {
            var result = EnumExtensions.SafeParse<SampleEnum>("WithDescription");
            Assert.That(result, Is.EqualTo(SampleEnum.WithDescription));
        }

        [Test]
        public void SafeParse_InvalidName_ReturnsDefault()
        {
            var result = EnumExtensions.SafeParse("Invalid", SampleEnum.NoDescription);
            Assert.That(result, Is.EqualTo(SampleEnum.NoDescription));
        }

        [Test]
        public void SafeParse_NullOrWhitespace_ReturnsDefault()
        {
            var result = EnumExtensions.SafeParse<SampleEnum>(null);
            Assert.That(result, Is.EqualTo(default(SampleEnum)));
        }

        [Test]
        public void SafeParse_CaseInsensitive_ReturnsValue()
        {
            var result = EnumExtensions.SafeParse<SampleEnum>("withdescription");
            Assert.That(result, Is.EqualTo(SampleEnum.WithDescription));
        }

        [Test]
        public void TryParseEnum_ValidName_ReturnsTrue()
        {
            var success = EnumExtensions.TryParseEnum<SampleEnum>("Another", out var result);
            Assert.That(success, Is.True);
            Assert.That(result, Is.EqualTo(SampleEnum.Another));
        }

        [Test]
        public void TryParseEnum_InvalidName_ReturnsFalse()
        {
            var success = EnumExtensions.TryParseEnum<SampleEnum>("Invalid", out _);
            Assert.That(success, Is.False);
        }

        [Test]
        public void GetValues_ReturnsAllValues()
        {
            var values = EnumExtensions.GetValues<SampleEnum>().ToList();
            Assert.That(values.Count, Is.EqualTo(3));
            Assert.That(values, Does.Contain(SampleEnum.WithDescription));
            Assert.That(values, Does.Contain(SampleEnum.NoDescription));
            Assert.That(values, Does.Contain(SampleEnum.Another));
        }
    }
}
