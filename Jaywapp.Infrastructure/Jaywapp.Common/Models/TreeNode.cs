using System.Collections.Generic;

namespace Jaywapp.Common.Models
{
    /// <summary>
    /// 제네릭 트리 노드를 나타내는 클래스입니다.
    /// </summary>
    /// <typeparam name="T">노드 값의 타입입니다.</typeparam>
    public class TreeNode<T>
    {
        private readonly List<TreeNode<T>> _children = new List<TreeNode<T>>();

        /// <summary>
        /// 노드의 값입니다.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// 부모 노드입니다. 루트 노드인 경우 null입니다.
        /// </summary>
        public TreeNode<T> Parent { get; private set; }

        /// <summary>
        /// 자식 노드 목록입니다.
        /// </summary>
        public IReadOnlyList<TreeNode<T>> Children => _children;

        /// <summary>
        /// 루트 노드인지 여부입니다.
        /// </summary>
        public bool IsRoot => Parent == null;

        /// <summary>
        /// 리프 노드인지 여부입니다.
        /// </summary>
        public bool IsLeaf => _children.Count == 0;

        /// <summary>
        /// 노드의 깊이입니다. 루트 노드는 0입니다.
        /// </summary>
        public int Depth
        {
            get
            {
                int depth = 0;
                var current = Parent;

                while (current != null)
                {
                    depth++;
                    current = current.Parent;
                }

                return depth;
            }
        }

        /// <summary>
        /// <see cref="TreeNode{T}"/>의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="value">노드의 값입니다.</param>
        public TreeNode(T value)
        {
            Value = value;
        }

        /// <summary>
        /// 자식 노드를 추가합니다.
        /// </summary>
        /// <param name="value">자식 노드의 값입니다.</param>
        /// <returns>추가된 자식 노드입니다.</returns>
        public TreeNode<T> AddChild(T value)
        {
            var child = new TreeNode<T>(value) { Parent = this };
            _children.Add(child);
            return child;
        }

        /// <summary>
        /// 자식 노드를 제거합니다.
        /// </summary>
        /// <param name="child">제거할 자식 노드입니다.</param>
        /// <returns>제거 성공 여부입니다.</returns>
        public bool RemoveChild(TreeNode<T> child)
        {
            if (child == null)
                return false;

            if (_children.Remove(child))
            {
                child.Parent = null;
                return true;
            }

            return false;
        }

        /// <summary>
        /// 트리를 탐색합니다.
        /// </summary>
        /// <param name="order">탐색 순서입니다. 기본값은 깊이 우선입니다.</param>
        /// <returns>탐색 순서대로 노드를 반환합니다.</returns>
        public IEnumerable<TreeNode<T>> Traverse(TraversalOrder order = TraversalOrder.DepthFirst)
        {
            if (order == TraversalOrder.BreadthFirst)
                return TraverseBreadthFirst();

            return TraverseDepthFirst();
        }

        private IEnumerable<TreeNode<T>> TraverseDepthFirst()
        {
            var stack = new Stack<TreeNode<T>>();
            stack.Push(this);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                yield return node;

                for (int i = node._children.Count - 1; i >= 0; i--)
                {
                    stack.Push(node._children[i]);
                }
            }
        }

        private IEnumerable<TreeNode<T>> TraverseBreadthFirst()
        {
            var queue = new Queue<TreeNode<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                yield return node;

                foreach (var child in node._children)
                {
                    queue.Enqueue(child);
                }
            }
        }
    }
}
