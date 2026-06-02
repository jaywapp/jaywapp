using Jaywapp.Common.Models;

namespace Jaywapp.Common.Tests.Models
{
    [TestFixture]
    public class TreeNodeTests
    {
        [Test]
        public void TreeNode_NewNode_IsRoot()
        {
            var node = new TreeNode<string>("root");
            Assert.That(node.IsRoot, Is.True);
            Assert.That(node.IsLeaf, Is.True);
            Assert.That(node.Depth, Is.EqualTo(0));
            Assert.That(node.Value, Is.EqualTo("root"));
        }

        [Test]
        public void AddChild_CreatesChild()
        {
            var root = new TreeNode<int>(1);
            var child = root.AddChild(2);

            Assert.That(root.Children.Count, Is.EqualTo(1));
            Assert.That(root.IsLeaf, Is.False);
            Assert.That(child.Parent, Is.SameAs(root));
            Assert.That(child.IsRoot, Is.False);
            Assert.That(child.Depth, Is.EqualTo(1));
        }

        [Test]
        public void RemoveChild_RemovesChild()
        {
            var root = new TreeNode<int>(1);
            var child = root.AddChild(2);
            var removed = root.RemoveChild(child);

            Assert.That(removed, Is.True);
            Assert.That(root.Children.Count, Is.EqualTo(0));
            Assert.That(child.Parent, Is.Null);
        }

        [Test]
        public void RemoveChild_NonChild_ReturnsFalse()
        {
            var root = new TreeNode<int>(1);
            var other = new TreeNode<int>(99);

            Assert.That(root.RemoveChild(other), Is.False);
        }

        [Test]
        public void RemoveChild_Null_ReturnsFalse()
        {
            var root = new TreeNode<int>(1);
            Assert.That(root.RemoveChild(null!), Is.False);
        }

        [Test]
        public void Traverse_DepthFirst_ReturnsCorrectOrder()
        {
            // Tree:
            //       1
            //      / \
            //     2   3
            //    /
            //   4
            var root = new TreeNode<int>(1);
            var child2 = root.AddChild(2);
            var child3 = root.AddChild(3);
            child2.AddChild(4);

            var values = root.Traverse(TraversalOrder.DepthFirst).Select(n => n.Value).ToList();
            Assert.That(values, Is.EqualTo(new[] { 1, 2, 4, 3 }));
        }

        [Test]
        public void Traverse_BreadthFirst_ReturnsCorrectOrder()
        {
            var root = new TreeNode<int>(1);
            var child2 = root.AddChild(2);
            var child3 = root.AddChild(3);
            child2.AddChild(4);

            var values = root.Traverse(TraversalOrder.BreadthFirst).Select(n => n.Value).ToList();
            Assert.That(values, Is.EqualTo(new[] { 1, 2, 3, 4 }));
        }

        [Test]
        public void Depth_MultiLevel_ReturnsCorrectDepth()
        {
            var root = new TreeNode<string>("a");
            var b = root.AddChild("b");
            var c = b.AddChild("c");
            var d = c.AddChild("d");

            Assert.That(root.Depth, Is.EqualTo(0));
            Assert.That(b.Depth, Is.EqualTo(1));
            Assert.That(c.Depth, Is.EqualTo(2));
            Assert.That(d.Depth, Is.EqualTo(3));
        }
    }
}
