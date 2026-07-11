using System.Data;
using Jaywapp.Infrastructure.Helpers;

namespace Jaywapp.Infrastructure.Tests.Helpers
{
    [TestFixture]
    public class TestDataTableHelper
    {
        private DataTable _testTable;

        [SetUp]
        public void Setup()
        {
            _testTable = new DataTable();
            _testTable.Columns.Add("Id", typeof(int));
            _testTable.Columns.Add("Name", typeof(string));
            _testTable.Rows.Add(1, "Alice");
            _testTable.Rows.Add(2, "Bob");
        }


        [TearDown]
        public void TearDown()
        {
            _testTable?.Dispose();
        }


        [Test]
        public void TestToDataColumnList_ReturnsCorrectColumns()
        {
            // Act
            var columns = _testTable.Columns.ToList();

            // Assert
            Assert.That(columns.Count, Is.EqualTo(2));
            Assert.That(columns[0].ColumnName, Is.EqualTo("Id"));
            Assert.That(columns[1].ColumnName, Is.EqualTo("Name"));
        }

        [Test]
        public void TestToDataRowList_ReturnsCorrectRows()
        {
            // Act
            var rows = _testTable.Rows.ToList();

            // Assert
            Assert.That(rows.Count, Is.EqualTo(2));
            Assert.That(rows[0]["Id"], Is.EqualTo(1));
            Assert.That(rows[1]["Name"], Is.EqualTo("Bob"));
        }
    }
}
