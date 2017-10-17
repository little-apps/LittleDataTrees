using System;

namespace Trees.Exceptions
{
    public class NodeAlreadyExistsException : Exception
    {
        public readonly object ExistingValue;

        /// <inheritdoc />
        /// <summary>
        /// Constructor for NodeAlreadyExistsException
        /// </summary>
        /// <param name="value">Value that already exists.</param>
        public NodeAlreadyExistsException(object value) : base("Node already exists with value.")
        {
            ExistingValue = value;
        }
    }
}
