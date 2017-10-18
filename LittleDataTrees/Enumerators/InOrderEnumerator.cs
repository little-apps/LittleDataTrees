using System;
using System.Collections;
using System.Collections.Generic;
using Trees.Abstract;

namespace LittleDataTrees.Enumerators
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

        /// <summary>
        /// The current node to use. This is initially set as the root of the tree.
        /// </summary>
        private TNode _currentNode;

        /// <summary>
        /// The current element.
        /// </summary>
        /// <remarks>This is not reflective of the <seealso cref="_currentNode"/> and is to be set to null when the object is instantiated.</remarks>
        public TValue Current { get; private set; }

        private bool HasNext => _stack.Count > 0 || Current != null;

        object IEnumerator.Current => !Equals(Current, default(TValue)) ? Current : (object) null;

        public InOrderEnumerator(BaseTree<TNode, TValue> tree)
        {
            _tree = tree ?? throw new ArgumentNullException(nameof(tree));
            _currentNode = tree.Root;
            Current = default(TValue);
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
                    if (_stack.Count == 0)
                        // Nothing left
                        break;

                    var leftMost = _stack.Pop();
                    _currentNode = leftMost.Right;

                    Current = leftMost.Value;

                    return true;
                }
            }

            Current = default(TValue);
            return false;
        }

        public void Reset()
        {
            _currentNode = _tree.Root;
            Current = default(TValue);
        }


        public IEnumerator GetEnumerator()
        {
            Reset();

            while (MoveNext())
                yield return Current;
        }
    }
}
