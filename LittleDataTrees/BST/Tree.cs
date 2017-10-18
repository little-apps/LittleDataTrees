using System;
using LittleDataTrees.Abstract;
using LittleDataTrees.Exceptions;

namespace LittleDataTrees.BST
{
    /// <summary>
    /// Binary Search Tree (BST)
    /// </summary>
    /// <typeparam name="T">Type of values in tree.</typeparam>
    public class Tree<T> : BaseTree<Node<T>, T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Number of items in tree.
        /// </summary>
        public uint Count { get; private set; }

        public override Node<T> Add(T value, object tag = null)
        {
            var nodeToAdd = new Node<T>(value);

            if (Root != null)
            {
                var nextNode = Root;

                while (nextNode != null)
                {
                    if (nodeToAdd.CompareTo(nextNode) < 0)
                    {
                        if (nextNode.Left == null)
                        {
                            // Set it to new node
                            nextNode.Left = nodeToAdd;
                            break;
                        }

                        nextNode = nextNode.Left;
                    }
                    else if (nodeToAdd.CompareTo(nextNode) > 0)
                    {
                        if (nextNode.Right == null)
                        {
                            // Set it to new node
                            nextNode.Right = nodeToAdd;
                            break;
                        }

                        nextNode = nextNode.Right;
                    }
                    else
                        // Node can't already exist
                        throw new NodeAlreadyExistsException(value);
                }
            }
            else
            {
                Root = nodeToAdd;
            }

            CalcHeights();
            Count++;


            return nodeToAdd;
        }

        /// <inheritdoc />
        /// <exception cref="NodeNotFoundException">Thrown if node with value isn't found.</exception>
        public override void Delete(T value)
        {
            Root = Delete(Root, value);

            CalcHeights();
            Count--;
        }

        /// <summary>
        /// Uses recursion to locate a node in the tree.
        /// </summary>
        /// <param name="currentNode">Current node in tree</param>
        /// <param name="value">Value to find.</param>
        /// <returns>New tree without value.</returns>
        private static Node<T> Delete(Node<T> currentNode, T value)
        {
            // If null -> node can't be found.
            if (currentNode == null)
                throw new NodeNotFoundException();

            var comparison = value.CompareTo(currentNode.Value);

            if (comparison == 0)
            {
                // At node to delete

                if (currentNode.Left == null && currentNode.Right == null)
                    // If node has 0 children -> have it set as null.
                    currentNode = null;
                else if (currentNode.Left != null && currentNode.Right == null)
                    // If node has a left child -> replace it with the left child.
                    currentNode = currentNode.Left;

                else if (currentNode.Left == null && currentNode.Right != null)
                    // If node has right child -> replace it with the right child.
                    currentNode = currentNode.Right;
                else if (currentNode.Left != null && currentNode.Right != null)
                {
                    // If node has a left and right child -> replace it with the smallest node in the right tree and delete the smallest node in the right tree.

                    var smallestNode = currentNode.Right.FindMin();

                    currentNode.Value = smallestNode.Value;

                    currentNode.Right = Delete(currentNode.Right, smallestNode.Value);
                }
            }
            else if (comparison < 0)
            {
                // Value is in the left subtree
                currentNode.Left = Delete(currentNode.Left, value);
            }
            else if (comparison > 0)
            {
                // Value is in the right subtree
                currentNode.Right = Delete(currentNode.Right, value);
            }

            return currentNode;
        }

        private void CalcHeights()
        {
            Height = 1;
            CalcHeights(Root, 1);
        }

        private void CalcHeights(Node<T> node, uint height)
        {
            node.Height = height;

            // Set tree height to this height (if it's higher)
            Height = Math.Max(Height, height);

            if (node.Left != null)
                CalcHeights(node.Left, height + 1);

            if (node.Right != null)
                CalcHeights(node.Right, height + 1);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value" /> is null.</exception>
        public override bool Contains(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var currentNode = Root;

            while (currentNode != null)
            {
                var comparison = value.CompareTo(currentNode.Value);

                if (comparison < 0)
                    currentNode = currentNode.Left;
                else if (comparison > 0)
                    currentNode = currentNode.Right;
                else
                    return true;
            }

            return false;
        }

        /// <inheritdoc />
        /// <exception cref="NodeNotFoundException">Thrown if node wasn't found in tree.</exception>
        public override Node<T> Find(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var currentNode = Root;

            while (currentNode != null)
            {
                var comparison = value.CompareTo(currentNode.Value);

                if (comparison < 0)
                    currentNode = currentNode.Left;
                else if (comparison > 0)
                    currentNode = currentNode.Right;
                else
                    return currentNode;
            }

            throw new NodeNotFoundException();
        }

        public override void Clear()
        {
            Root = null;
            Count = 0;
        }
    }
}
