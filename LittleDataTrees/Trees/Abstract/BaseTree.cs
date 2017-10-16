using System;
using System.IO;
using Trees.Enumerators;
using Trees.Misc;

namespace Trees.Abstract
{
    /// <summary>
    /// Abstract tree class
    /// </summary>
    /// <typeparam name="TValue">Type of values in tree (must implement <seealso cref="IComparable{TValue}"/></typeparam>
    /// <typeparam name="TNode">Type of node for tree (must inherit <seealso cref="BaseTreeNode{TNode,TValue}"/>)</typeparam>
    public abstract class BaseTree<TNode, TValue>
        where TNode : BaseTreeNode<TNode, TValue>
        where TValue : IComparable<TValue>
    {
        /// <summary>
        /// Root node of tree.
        /// </summary>
        public TNode Root { get; protected set; }
        
        /// <summary>
        /// If true, tree root is null.
        /// </summary>
        public bool IsEmpty => Root == null;

        public InOrderEnumerator<TNode, TValue> InOrder => new InOrderEnumerator<TNode, TValue>(this);
        public PreOrderEnumerator<TNode, TValue> PreOrder => new PreOrderEnumerator<TNode, TValue>(this);
        public PostOrderEnumerator<TNode, TValue> PostOrder => new PostOrderEnumerator<TNode, TValue>(this);

        /// <summary>
        /// Constructor for BaseTree.
        /// </summary>
        /// <remarks>Sets <seealso cref="Root"/> to it's default value.</remarks>
        protected BaseTree()
        {
            Root = default(TNode);
        }

        /// <summary>
        /// Outputs the tree vertically to <paramref name="textWriter"/>
        /// </summary>
        /// <param name="textWriter"><seealso cref="TextWriter"/> to write to</param>
        public void PrintVertical(TextWriter textWriter)
        {
            var output = new Output<TNode, TValue>(this, textWriter);
            output.PrintVertical();
        }

        /// <summary>
        /// Outputs the tree horizontally to <paramref name="textWriter"/>
        /// </summary>
        /// <param name="textWriter"><seealso cref="TextWriter"/> to write to</param>
        public void PrintHorizontal(TextWriter textWriter)
        {
            var output = new Output<TNode, TValue>(this, textWriter);
            output.PrintHorizontal();
        }
    }
}
