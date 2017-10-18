using System;

namespace LittleDataTrees.Abstract
{
    /// <summary>
    /// Abstract class for tree nodes (with left and right nodes)
    /// </summary>
    /// <typeparam name="TNode">Type to use for left and right nodes and is usually the parent class type (must inherit this class, <seealso cref="BaseTreeNode{TNode,TValue}"/>)</typeparam>
    /// <typeparam name="TValue">Type of values (must implement <seealso cref="IComparable{TValue}"/>)</typeparam>
    public abstract class BaseTreeNode<TNode, TValue> : IBaseNode<TValue> 
        where TNode : BaseTreeNode<TNode, TValue>
        where TValue : IComparable<TValue>
    {
        /// <summary>
        /// The node on the left of this node.
        /// </summary>
        public TNode Left { get; internal set; }
        /// <summary>
        /// The node on the right of this node.
        /// </summary>
        public TNode Right { get; internal set; }
        /// <summary>
        /// The value of this node.
        /// </summary>
        public TValue Value { get; internal set; }

        /// <summary>
        /// The height of the node in the tree
        /// </summary>
        public uint Height { get; internal set; }

        /// <summary>
        /// Constructor for BaseTreeNode. Sets Value and Left/Right to null.
        /// </summary>
        /// <param name="value">Value to set (can be null)</param>
        protected BaseTreeNode(TValue value)
        {
            Value = value;

            Left = default(TNode);
            Right = default(TNode);
        }
        
        public abstract int CompareTo(IBaseNode<TValue> other);
    }
}
