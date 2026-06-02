using Jaywapp.Common.Models;

namespace Jaywapp.Common.Tests.Models
{
    [TestFixture]
    public class PageTests
    {
        [Test]
        public void PageRequest_ValidParams_CreatesInstance()
        {
            var request = new PageRequest(2, 10);
            Assert.That(request.Page, Is.EqualTo(2));
            Assert.That(request.PageSize, Is.EqualTo(10));
            Assert.That(request.Skip, Is.EqualTo(10));
            Assert.That(request.Sorts, Is.Not.Null);
            Assert.That(request.Sorts.Count, Is.EqualTo(0));
        }

        [Test]
        public void PageRequest_WithSorts_PreservesSorts()
        {
            var sorts = new[] { new SortDefinition("Name", SortDirection.Descending) };
            var request = new PageRequest(1, 20, sorts);
            Assert.That(request.Sorts.Count, Is.EqualTo(1));
            Assert.That(request.Sorts[0].Field, Is.EqualTo("Name"));
            Assert.That(request.Sorts[0].Direction, Is.EqualTo(SortDirection.Descending));
        }

        [Test]
        public void PageRequest_InvalidPage_ThrowsArgumentOutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PageRequest(0, 10));
        }

        [Test]
        public void PageRequest_InvalidPageSize_ThrowsArgumentOutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PageRequest(1, 0));
        }

        [Test]
        public void PageResult_BasicProperties()
        {
            var items = new[] { "a", "b", "c" };
            var result = new PageResult<string>(items, 10, 1, 3);

            Assert.That(result.Items.Count, Is.EqualTo(3));
            Assert.That(result.TotalCount, Is.EqualTo(10));
            Assert.That(result.Page, Is.EqualTo(1));
            Assert.That(result.PageSize, Is.EqualTo(3));
            Assert.That(result.TotalPages, Is.EqualTo(4));
            Assert.That(result.HasNextPage, Is.True);
            Assert.That(result.HasPreviousPage, Is.False);
        }

        [Test]
        public void PageResult_LastPage_NoNextPage()
        {
            var items = new[] { "x" };
            var result = new PageResult<string>(items, 5, 3, 2);

            Assert.That(result.TotalPages, Is.EqualTo(3));
            Assert.That(result.HasNextPage, Is.False);
            Assert.That(result.HasPreviousPage, Is.True);
        }

        [Test]
        public void PageResult_NullItems_ThrowsArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => new PageResult<string>(null!, 10, 1, 5));
        }

        [Test]
        public void SortDefinition_ValidField_CreatesInstance()
        {
            var sort = new SortDefinition("Name");
            Assert.That(sort.Field, Is.EqualTo("Name"));
            Assert.That(sort.Direction, Is.EqualTo(SortDirection.Ascending));
        }

        [Test]
        public void SortDefinition_EmptyField_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new SortDefinition(""));
            Assert.Throws<ArgumentException>(() => new SortDefinition(null!));
        }
    }
}
