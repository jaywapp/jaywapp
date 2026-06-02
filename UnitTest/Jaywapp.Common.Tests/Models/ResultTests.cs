using Jaywapp.Common.Models;

namespace Jaywapp.Common.Tests.Models
{
    [TestFixture]
    public class ResultTests
    {
        [Test]
        public void Success_IsSuccessTrue()
        {
            var result = Result.Success();
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.IsFailure, Is.False);
            Assert.That(result.Error, Is.Null);
        }

        [Test]
        public void Failure_IsSuccessFalse()
        {
            var result = Result.Failure(new Error("ERR01", "오류 발생"));
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.Not.Null);
            Assert.That(result.Error.Code, Is.EqualTo("ERR01"));
        }

        [Test]
        public void Failure_WithCodeAndMessage()
        {
            var result = Result.Failure("ERR02", "다른 오류");
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error.Message, Is.EqualTo("다른 오류"));
        }

        [Test]
        public void Failure_NullError_ThrowsArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Result.Failure(null!));
        }

        [Test]
        public void Success_Generic_ReturnsValue()
        {
            var result = Result.Success(42);
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value, Is.EqualTo(42));
        }

        [Test]
        public void Failure_Generic_ValueThrowsOnAccess()
        {
            var result = Result.Failure<int>(new Error("ERR", "fail"));
            Assert.That(result.IsFailure, Is.True);
            Assert.Throws<InvalidOperationException>(() => { var _ = result.Value; });
        }

        [Test]
        public void Error_Create_ReturnsSimpleError()
        {
            var error = Error.Create("E001", "테스트 오류");
            Assert.That(error.Code, Is.EqualTo("E001"));
            Assert.That(error.Message, Is.EqualTo("테스트 오류"));
            Assert.That(error.Exception, Is.Null);
            Assert.That(error.Metadata.Count, Is.EqualTo(0));
        }

        [Test]
        public void Error_WithException_PreservesException()
        {
            var ex = new InvalidOperationException("test");
            var error = new Error("E002", "예외 포함", ex);
            Assert.That(error.Exception, Is.SameAs(ex));
        }

        [Test]
        public void Error_WithMetadata_PreservesMetadata()
        {
            var metadata = new Dictionary<string, object> { { "key", "value" } };
            var error = new Error("E003", "메타데이터", metadata: metadata);
            Assert.That(error.Metadata["key"], Is.EqualTo("value"));
        }

        [Test]
        public void Error_ToString_ReturnsCodeAndMessage()
        {
            var error = new Error("ERR", "메시지");
            Assert.That(error.ToString(), Is.EqualTo("ERR: 메시지"));
        }
    }
}
