using System;
using System.Collections.Generic;
using System.IO;
using LittleDataTrees.Enumerators;
using LittleDataTrees.Misc;

namespace LittleDataTrees.Abstract
{
    /// <summary>
    /// Abstract tree class
    /// </summary>
    /// <typeparam name="TValue">Type of values in tree (must implement <seealso cref="IComparable{TValue}"/></typeparam>
    /// <typeparam name="TNode">Type of node for tree (must inherit <seealso cref="BaseTreeNode{TNode,TValue}"/>)</typeparam>
    public abstract class BaseTree<TNode, TValue>
        where TNode : BaseTreeNode<TValue>
        where TValue : IComparable<TValue>
    {
        /// <summary>
        /// Root node of tree.
        /// </summary>
        public TNode Root { get; protected set; }

        /// <summary>
        /// Gets the height of the tree.
        /// </summary>
        public uint Height { get; protected set; }
        
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
        /// Constructor for populating BaseTree with existing collection
        /// </summary>
        /// <param name="collection">Collection to populate tree with.</param>
        /// <exception cref="ArgumentNullException">Thrown if collection is null.</exception>
        public BaseTree(IEnumerable<TValue> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            AddRange(collection);
        }

        /// <summary>
        /// Adds node with value to tree.
        /// </summary>
        /// <param name="value">Value to add</param>
        /// <param name="tag">Option tag object to associate with node (default is null)</param>
        /// <returns>Node that was added.</returns>
        public abstract TNode Add(TValue value, object tag = null);

        /// <summary>
        /// Adds an existing collection to the tree.
        /// </summary>
        /// <param name="collection">Existing collection</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="collection"/> is null.</exception>
        public virtual void AddRange(IEnumerable<TValue> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            foreach (var value in collection)
            {
                Add(value);
            }
        }

        /// <summary>
        /// Deletes node with value from tree.
        /// </summary>
        /// <param name="value">Value to delete</param>
        public abstract void Delete(TValue value);

        /// <summary>
        /// Checks if value exists in tree.
        /// </summary>
        /// <param name="value">Value to check exists.</param>
        /// <returns>True if it exists.</returns>
        public abstract bool Contains(TValue value);

        /// <summary>
        /// Finds node with value in tree.
        /// </summary>
        /// <param name="value">Value to find.</param>
        /// <returns>Node in tree with value.</returns>
        public abstract TNode Find(TValue value);

        /// <summary>
        /// Clears all nodes from tree
        /// </summary>
        public abstract void Clear();

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
