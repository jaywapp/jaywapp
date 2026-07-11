using Jaywapp.Common.Models;

namespace Jaywapp.Common.Tests.Models
{
    [TestFixture]
    public class ChangeTrackingTests
    {
        [Test]
        public void ChangeSet_WithItems_HasChanges()
        {
            var cs = new ChangeSet<string>(
                new[] { "added1" },
                new[] { "updated1" },
                new[] { "removed1" });

            Assert.That(cs.HasChanges, Is.True);
            Assert.That(cs.TotalChanges, Is.EqualTo(3));
            Assert.That(cs.Added.Count, Is.EqualTo(1));
            Assert.That(cs.Updated.Count, Is.EqualTo(1));
            Assert.That(cs.Removed.Count, Is.EqualTo(1));
        }

        [Test]
        public void ChangeSet_Empty_NoChanges()
        {
            var cs = ChangeSet<int>.Empty();

            Assert.That(cs.HasChanges, Is.False);
            Assert.That(cs.TotalChanges, Is.EqualTo(0));
        }

        [Test]
        public void ChangeSet_NullParams_TreatedAsEmpty()
        {
            var cs = new ChangeSet<int>(null, null, null);

            Assert.That(cs.HasChanges, Is.False);
            Assert.That(cs.Added.Count, Is.EqualTo(0));
            Assert.That(cs.Updated.Count, Is.EqualTo(0));
            Assert.That(cs.Removed.Count, Is.EqualTo(0));
        }

        [Test]
        public void Trackable_Initial_NotDirty()
        {
            var t = new Trackable<int>(42);

            Assert.That(t.Original, Is.EqualTo(42));
            Assert.That(t.Current, Is.EqualTo(42));
            Assert.That(t.IsDirty, Is.False);
        }

        [Test]
        public void Trackable_Changed_IsDirty()
        {
            var t = new Trackable<string>("original");
            t.Current = "modified";

            Assert.That(t.IsDirty, Is.True);
        }

        [Test]
        public void Trackable_AcceptChanges_OriginalUpdated()
        {
            var t = new Trackable<int>(1);
            t.Current = 2;
            t.AcceptChanges();

            Assert.That(t.Original, Is.EqualTo(2));
            Assert.That(t.Current, Is.EqualTo(2));
            Assert.That(t.IsDirty, Is.False);
        }

        [Test]
        public void Trackable_RejectChanges_CurrentReverted()
        {
            var t = new Trackable<int>(1);
            t.Current = 2;
            t.RejectChanges();

            Assert.That(t.Original, Is.EqualTo(1));
            Assert.That(t.Current, Is.EqualTo(1));
            Assert.That(t.IsDirty, Is.False);
        }

        [Test]
        public void Trackable_NullValue_HandledCorrectly()
        {
            var t = new Trackable<string?>(null);
            Assert.That(t.IsDirty, Is.False);

            t.Current = "value";
            Assert.That(t.IsDirty, Is.True);

            t.RejectChanges();
            Assert.That(t.Current, Is.Null);
            Assert.That(t.IsDirty, Is.False);
        }
    }
}
