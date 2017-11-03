using System;
using LittleDataTrees.Abstract;

namespace LittleDataTrees.AVL
{
    public class Node<T> : LeftRightNode<Node<T>, T> 
        where T : IComparable<T>
    {

        public object Tag;

        public int MaxDepth
        {
            get
            {
                var left = Left?.MaxDepth ?? 0;
                var right = Right?.MaxDepth ?? 0;

                return Math.Max(left, right) + 1;
            }
        }

        public Node(T value) : base(value)
        {
        }

        /// <summary>
        /// Checks if this node or any of its child nodes contains the value.
        /// </summary>
        /// <param name="value">Value to search for</param>
        /// <returns>True if this node or a child of it contains <paramref name="value"/></returns>
        public bool Contains(T value)
        {
            var comparison = value.CompareTo(Value);

            if (comparison == 0)
                return true;

            var nextNode = comparison < 0 ? Left : Right;

            return nextNode != null && nextNode.Contains(value);
        }

        /// <summary>
        /// Finds node with value in this node or it's children
        /// </summary>
        /// <param name="value">Value to search for</param>
        /// <returns>Node where value exists or null if it wasn't found.</returns>
        public Node<T> Find(T value)
        {
            var currentNode = this;

            while (true)
            {
                if (currentNode == null)
                    return null;

                var comparison = value.CompareTo(currentNode.Value);

                if (comparison < 0)
                    currentNode = currentNode.Left;
                else if (comparison > 0)
                    currentNode = currentNode.Right;
                else
                    return currentNode;
            }
        }

        public override int CompareTo(IBaseNode<T> other)
        {
            if (ReferenceEquals(other, null))
                return -1;

            if (ReferenceEquals(this, other))
                return 0;

            return Value.CompareTo(other.Value);
        }

        
    }
}
