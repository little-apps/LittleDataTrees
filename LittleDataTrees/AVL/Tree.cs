using System;
using LittleDataTrees.Abstract;
using LittleDataTrees.Exceptions;

namespace LittleDataTrees.AVL
{
    public class Tree<T> : BaseTree<Node<T>, T> where T : IComparable<T>
    {
        public uint Count { get; protected set; }

        public Tree()
        {
            Root = null;
        }

        public override Node<T> Add(T value, object tag = null)
        {
            var newNode = new Node<T>(value) { Tag = tag };

            Root = Add(Root, newNode);

            CalcHeights();

            Count++;

            return newNode;
        }

        private static Node<T> Add(Node<T> current, Node<T> nodeToAdd)
        {
            if (current == null)
                // The left/right node is null, so we can add it there
                return nodeToAdd;

            if (nodeToAdd.CompareTo(current) < 0)
            {
                current.Left = Add(current.Left, nodeToAdd);
                current = BalanceTree(current);
            }
            else if (nodeToAdd.CompareTo(current) > 0)
            {
                current.Right = Add(current.Right, nodeToAdd);
                current = BalanceTree(current);
            }
            else if (nodeToAdd.CompareTo(current) == 0)
            {
                throw new NodeAlreadyExistsException(current.Value);
            }
                
            return current;
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

        private static Node<T> BalanceTree(Node<T> node)
        {
            var balance = DetermineBalance(node);

            if (balance >= 2)
                node = DetermineBalance(node.Left) > 0 ? RotateLeftLeft(node) : RotateLeftRight(node);
            else if (balance <= -2)
                node = DetermineBalance(node.Right) > 0 ? RotateRightLeft(node) : RotateRightRight(node);

            return node;
        }

        private static int DetermineBalance(Node<T> current)
        {
            var l = current.Left?.MaxDepth ?? 0;
            var r = current.Right?.MaxDepth ?? 0;

            return l - r;
        }

        private static Node<T> RotateRightRight(Node<T> node)
        {
            var pivot = node.Right;
            node.Right = pivot.Left;
            pivot.Left = node;
            return pivot;
        }

        private static Node<T> RotateLeftLeft(Node<T> node)
        {
            var pivot = node.Left;
            node.Left = pivot.Right;
            pivot.Right = node;
            return pivot;
        }

        private static Node<T> RotateLeftRight(Node<T> node)
        {
            var pivot = node.Left;
            node.Left = RotateRightRight(pivot);
            return RotateLeftLeft(node);
        }

        private static Node<T> RotateRightLeft(Node<T> node)
        {
            var pivot = node.Right;
            node.Right = RotateLeftLeft(pivot);
            return RotateRightRight(node);
        }

        /// <inheritdoc />
        public override bool Contains(T value)
        {
            return !IsEmpty && Root.Contains(value);
        }

        /// <inheritdoc />
        /// <exception cref="NodeNotFoundException">Thrown if node wasn't found in tree.</exception>
        public override Node<T> Find(T value)
        {
            return Root?.Find(value) ?? throw new NodeNotFoundException();
        }

        public override void Clear()
        {
            Root = null;
            Count = 0;
        }

        /// <inheritdoc />
        /// <exception cref="NodeNotFoundException">Thrown if node was not found in tree.</exception>
        public override void Delete(T value)
        {
            Root = Delete(Root, value);

            CalcHeights();

            Count--;
        }

        private static Node<T> Delete(Node<T> current, T value)
        {
            if (current == null)
                // Node not found
                throw new NodeNotFoundException();

            if (value.CompareTo(current.Value) < 0)
            {
                current.Left = Delete(current.Left, value);
                if (DetermineBalance(current) == -2)
                    current = DetermineBalance(current.Right) <= 0
                        ? RotateRightRight(current)
                        : RotateRightLeft(current);
            }
            else if (value.CompareTo(current.Value) > 0)
            {
                current.Right = Delete(current.Right, value);
                if (DetermineBalance(current) == 2)
                    current = DetermineBalance(current.Left) >= 0 ? RotateLeftLeft(current) : RotateLeftRight(current);
            }
            else
            {
                if (current.Right != null)
                {
                    var parent = current.Right;

                    while (parent.Left != null)
                        parent = parent.Left;

                    current.Value = parent.Value;
                    current.Right = Delete(current.Right, parent.Value);

                    if (DetermineBalance(current) == 2)
                        current = DetermineBalance(current.Left) >= 0
                            ? RotateLeftLeft(current)
                            : RotateLeftRight(current);
                }
                else
                {
                    return current.Left;
                }
            }

            return current;
        }
        
    }
}
