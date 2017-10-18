using System;

namespace LittleDataTrees.Abstract
{
    /// <inheritdoc />
    /// <summary>
    /// Interface for nodes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseNode<T> : IComparable<IBaseNode<T>> where T : IComparable<T>
    {
        /// <summary>
        /// Value for node.
        /// </summary>
        T Value { get; }
    }
}
