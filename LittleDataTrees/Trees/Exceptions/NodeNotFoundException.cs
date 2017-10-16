using System;

namespace Trees.Exceptions
{
    public class NodeNotFoundException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        /// Constructor for NodeNotFoundException
        /// </summary>
        public NodeNotFoundException() : base("Node could not be found.")
        {
            
        }
    }
}
