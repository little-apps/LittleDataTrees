using System;

namespace LittleDataTrees.Abstract
{
    public abstract class LeftRightNode<TNode, TValue> : BaseTreeNode<TValue>
        where TNode : LeftRightNode<TNode, TValue>
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

        public LeftRightNode(TValue value) : base(value)
        {
            Left = null;
            Right = null;
        }
    }
}
