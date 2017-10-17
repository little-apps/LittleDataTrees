using System;
using System.Collections;
using System.Collections.Generic;
using Trees.Abstract;

namespace Trees.Enumerators
{
    /// <summary>
    /// Class for enumerating through tree in post-order
    /// </summary>
    /// <typeparam name="TNode">Type of node (must inherit <see cref="BaseTreeNode{TNode,TValue}"/>)</typeparam>
    /// <typeparam name="TValue">Type of values (must implement <see cref="IComparable{TValue}"/>)</typeparam>
    public class PostOrderEnumerator<TNode, TValue> : IEnumerator<TValue>, IEnumerable
        where TNode : BaseTreeNode<TNode, TValue>
        where TValue : IComparable<TValue>
    {
        private readonly BaseTree<TNode, TValue> _tree;
        private readonly Stack<TNode> _stack = new Stack<TNode>();

        public TValue Current { get; private set; }

        private bool HasNext => _stack.Count > 0;

        object IEnumerator.Current => Current;

        private TNode _currentNode;
        private TNode _nextNode;
        private bool _leftLoop = true;
        private bool _done = false;

        /// <summary>
        /// Constructor for pre-order enumerator
        /// </summary>
        /// <param name="tree">Tree to enumerate through</param>
        public PostOrderEnumerator(BaseTree<TNode, TValue> tree)
        {
            _tree = tree ?? throw new ArgumentNullException(nameof(tree));
            _nextNode = tree.Root;
        }

        public void Dispose()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Moves to the left, right, and then root node.
        /// </summary>
        /// <returns>True if another node exists</returns>
        public bool MoveNext()
        {
            if (_done)
                return false;

            while (true)
            {
                if (_leftLoop)
                {
                    // Go through all left nodes until we get to leaf

                    _currentNode = _nextNode;

                    while (_currentNode != null)
                    {
                        _stack.Push(_currentNode);
                        _currentNode = _currentNode.Left;
                    }
                }

                if (_stack.Count > 0 && _currentNode == _stack.Peek().Right)
                {
                    _currentNode = _stack.Pop();
                    Current = _currentNode.Value;

                    _leftLoop = false;

                    return true;
                }

                if (_stack.Count == 0)
                {
                    _done = true;
                    return false;
                }
                
                _nextNode = _stack.Peek().Right;

                // Since we're on the right node, try to go traverse through all of it's left nodes next time
                _leftLoop = true;
            }

            // Won't ever reach here.
            // return false;
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
