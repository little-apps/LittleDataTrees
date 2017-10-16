using System;
using System.Collections;
using System.Collections.Generic;
using Trees.Abstract;

namespace Trees.Enumerators
{
    /// <summary>
    /// Class for enumerating through tree in-order
    /// </summary>
    /// <typeparam name="TNode">Type of node (must inherit <see cref="BaseTreeNode{TNode,TValue}"/>)</typeparam>
    /// <typeparam name="TValue">Type of values (must implement <see cref="IComparable{TValue}"/>)</typeparam>
    /// <example>If the tree contains 5, 3, 7, 4, 1, 2, 8, 9, 6, it will be iterated through as 1, 2, 3, 4, 5, 6, 7, 8, 9.</example>
    public class InOrderEnumerator<TNode, TValue> : IEnumerator<TValue>, IEnumerable
        where TNode : BaseTreeNode<TNode, TValue>
        where TValue : IComparable<TValue>
    {
        private readonly BaseTree<TNode, TValue> _tree;
        private readonly Stack<TNode> _stack = new Stack<TNode>();
        private TNode _currentNode;

        public TValue Current => _currentNode.Value;

        private bool HasNext => _stack.Count > 0 || Current != null;

        object IEnumerator.Current => Current;

        public InOrderEnumerator(BaseTree<TNode, TValue> tree)
        {
            _tree = tree ?? throw new ArgumentNullException(nameof(tree));
            _currentNode = tree.Root;
        }

        public void Dispose()
        {
            _currentNode = null;
        }

        /// <inheritdoc />
        /// <summary>
        /// Moves to the left, root, and then right node.
        /// </summary>
        /// <returns>True if another node exists</returns>
        public bool MoveNext()
        {
            while (HasNext)
            {
                if (_currentNode != null)
                {
                    _stack.Push(_currentNode);
                    _currentNode = _currentNode.Left;
                }
                else
                {
                    var leftMost = _stack.Pop();
                    _currentNode = leftMost.Right;

                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            _currentNode = _tree.Root;
        }


        public IEnumerator GetEnumerator()
        {
            Reset();

            while (MoveNext())
                yield return Current;
        }
    }
}
