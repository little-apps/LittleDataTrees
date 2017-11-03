using System;
using LittleDataTrees.Abstract;

namespace LittleDataTrees.BST
{
    public class Node<T> : LeftRightNode<Node<T>, T>
        where T : IComparable<T>
    {
        /// <summary>
        /// The node on the left of this node.
        /// </summary>
        public new Node<T> Left { get; internal set; }
        /// <summary>
        /// The node on the right of this node.
        /// </summary>
        public new Node<T> Right { get; internal set; }

        public Node(T value) : base(value)
        {
        }

        /// <summary>
        /// Finds the leftmost node
        /// </summary>
        /// <returns>Left most node (node with smallest value)</returns>
        public Node<T> FindMin()
        {
            // Go to the 
            return Left != null ? Left.FindMin() : this;
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
