using Jaywapp.Common.Extensions;

namespace Jaywapp.Common.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [TestCase("hello", true)]
        [TestCase("  hello  ", true)]
        [TestCase("", false)]
        [TestCase("   ", false)]
        [TestCase(null, false)]
        public void HasValue_ReturnsExpected(string? input, bool expected)
        {
            Assert.That(input.HasValue(), Is.EqualTo(expected));
        }

        [Test]
        public void SafeTruncate_ShorterThanMax_ReturnsOriginal()
        {
            Assert.That("abc".SafeTruncate(10), Is.EqualTo("abc"));
        }

        [Test]
        public void SafeTruncate_LongerThanMax_Truncates()
        {
            Assert.That("abcdef".SafeTruncate(3), Is.EqualTo("abc"));
        }

        [Test]
        public void SafeTruncate_Null_ReturnsNull()
        {
            Assert.That(((string?)null).SafeTruncate(5), Is.Null);
        }

        [Test]
        public void SafeTruncate_ZeroLength_ReturnsEmpty()
        {
            Assert.That("abc".SafeTruncate(0), Is.EqualTo(""));
        }

        [Test]
        public void SafeTruncate_NegativeLength_ThrowsArgumentOutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => "abc".SafeTruncate(-1));
        }

        [Test]
        public void ToSha256_KnownInput_ReturnsExpectedHash()
        {
            // SHA256("hello") = 2cf24dba5fb0a30e26e83b2ac5b9e29e1b161e5c1fa7425e73043362938b9824
            var result = "hello".ToSha256();
            Assert.That(result, Is.EqualTo("2cf24dba5fb0a30e26e83b2ac5b9e29e1b161e5c1fa7425e73043362938b9824"));
        }

        [Test]
        public void ToSha256_Null_ThrowsArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => ((string)null!).ToSha256());
        }

        [Test]
        public void ToSha256_EmptyString_ReturnsValidHash()
        {
            var result = "".ToSha256();
            Assert.That(result, Has.Length.EqualTo(64));
        }

        [Test]
        public void NormalizeLineEndings_MixedEndings_Normalizes()
        {
            var input = "line1\r\nline2\rline3\nline4";
            var result = input.NormalizeLineEndings();
            Assert.That(result, Is.EqualTo("line1\nline2\nline3\nline4"));
        }

        [Test]
        public void NormalizeLineEndings_CustomNewLine()
        {
            var input = "line1\r\nline2";
            var result = input.NormalizeLineEndings("\r\n");
            Assert.That(result, Is.EqualTo("line1\r\nline2"));
        }

        [Test]
        public void NormalizeLineEndings_Null_ReturnsNull()
        {
            Assert.That(((string?)null).NormalizeLineEndings(), Is.Null);
        }
    }
}
