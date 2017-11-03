using System;

namespace LittleDataTrees.Abstract
{
    /// <summary>
    /// Abstract class for tree nodes (with left and right nodes)
    /// </summary>
    /// <typeparam name="TValue">Type of values (must implement <seealso cref="IComparable{TValue}"/>)</typeparam>
    public abstract class BaseTreeNode<TValue> : IBaseNode<TValue> 
        where TValue : IComparable<TValue>
    {
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
        }
        
        public abstract int CompareTo(IBaseNode<TValue> other);
    }
}
