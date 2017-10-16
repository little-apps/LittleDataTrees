using System;
using System.Collections;
using System.Collections.Generic;
using Trees.Abstract;

namespace Trees.Enumerators
{
    /// <summary>
    /// Class for enumerating through tree in pre-order
    /// </summary>
    /// <typeparam name="TNode">Type of node (must inherit <see cref="BaseTreeNode{TNode,TValue}"/>)</typeparam>
    /// <typeparam name="TValue">Type of values (must implement <see cref="IComparable{TValue}"/>)</typeparam>
    public class PreOrderEnumerator<TNode, TValue> : IEnumerator<TValue>, IEnumerable
        where TNode : BaseTreeNode<TNode, TValue>
        where TValue : IComparable<TValue>
    {
        private readonly BaseTree<TNode, TValue> _tree;
        private readonly Stack<TNode> _stack = new Stack<TNode>();

        public TValue Current { get; private set; }

        private bool HasNext => _stack.Count > 0;

        object IEnumerator.Current => Current;

        /// <summary>
        /// Constructor for pre-order enumerator
        /// </summary>
        /// <param name="tree">Tree to enumerate through</param>
        public PreOrderEnumerator(BaseTree<TNode, TValue> tree)
        {
            _tree = tree ?? throw new ArgumentNullException(nameof(tree));
            _stack.Push(tree.Root);
        }

        public void Dispose()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Moves to the root, left, and then right node.
        /// </summary>
        /// <returns>True if another node exists</returns>
        public bool MoveNext()
        {
            if (!HasNext)
                return false;

            // Get node on top of stack
            var ret = _stack.Pop();

            // If node on top of the stack has right node -> add the right node to the top of the stack
            if (ret.Right != null)
                _stack.Push(ret.Right);

            // If node on top of the stack has left node -> add the left node to the top of the stack
            if (ret.Left != null)
                _stack.Push(ret.Left);

            // Current is the node on popped from top of stack
            Current = ret.Value;

            return true;
        }

        public void Reset()
        {
            _stack.Clear();
            _stack.Push(_tree.Root);
        }
        
        public IEnumerator GetEnumerator()
        {
            Reset();

            while (MoveNext())
                yield return Current;
        }
    }
}
