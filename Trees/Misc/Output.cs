using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Trees.Abstract;

namespace Trees.Misc
{
    public class Output<TNode, TValue>
        where TValue : IComparable<TValue>
        where TNode : BaseTreeNode<TNode, TValue>
    {
        private readonly BaseTree<TNode, TValue> _tree;
        private readonly TextWriter _textWriter;

        /// <summary>
        /// Constructor for Output class.
        /// </summary>
        /// <param name="tree">Tree instance to print.</param>
        /// <param name="textWriter"><seealso cref="TextWriter"/> to print to.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="tree"/> or <paramref name="textWriter"/> is null.</exception>
        public Output(BaseTree<TNode, TValue> tree, TextWriter textWriter)
        {
            _tree = tree ?? throw new ArgumentNullException(nameof(tree), "Tree cannot be null");
            _textWriter = textWriter ?? throw new ArgumentNullException(nameof(textWriter), "Text writer cannot be null");
        }

        /// <summary>
        /// Prints the tree horizontally
        /// </summary>
        public void PrintHorizontal()
        {
            PrintTree(_tree.Root);
        }

        /// <summary>
        /// Prints a tree node
        /// </summary>
        /// <param name="node">Node to print</param>
        private void PrintTree(TNode node)
        {
            if (node.Right != null)
            {
                PrintTree(node.Right, true, "");
            }

            PrintNodeValue(node);

            if (node.Left != null)
            {
                PrintTree(node.Left, false, "");
            }
        }

        /// <summary>
        /// <![CDATA[Prints node value or '<null>' if its null.]]>
        /// </summary>
        /// <param name="node"></param>
        private void PrintNodeValue(TNode node)
        {
            if (node.Value == null)
            {
                _textWriter.Write("<null>");
            }
            else
            {
                _textWriter.Write(node.Value);
            }

            _textWriter.WriteLine();
        }
        
        /// <summary>
        /// Prints the tree lines to node and node value.
        /// </summary>
        /// <param name="node">Node to print</param>
        /// <param name="isRight">Is it a right node?</param>
        /// <param name="indent">Indent to use</param>
        private void PrintTree(TNode node, bool isRight, string indent)
        {
            // use string and not stringbuffer on purpose as we need to change the indent at each recursion

            // Recurse through all the right nodes and print them
            if (node.Right != null)
                PrintTree(node.Right, true, indent + (isRight ? "        " : " |      "));

            // Print indent, line pointing from root to current node, and node value
            _textWriter.Write(indent);
            _textWriter.Write(isRight ? " /" : " \\");
            _textWriter.Write("----- ");
            PrintNodeValue(node);

            // Recurse through all the left nodes and print them
            if (node.Left != null)
                PrintTree(node.Left, false, indent + (isRight ? " |      " : "        "));
        }

        /// <summary>
        /// Prints tree vertically
        /// </summary>
        /// <remarks>Printing a tree vertically is a bit more tricky and it may not be formatted correctly if the node string values are long.</remarks>
        public void PrintVertical()
        {
            var nodes = new List<TNode> { _tree.Root };
            var level = 1;
            var maxLevel = MaxLevel(_tree.Root);

            while (true)
            {
                if (!nodes.Any() || nodes.All(node => node == null))
                    return;

                var floor = maxLevel - level;
                var endgeLines = (int)Math.Pow(2, Math.Max(floor - 1, 0));
                var firstSpaces = (int)Math.Pow(2, floor) - 1;
                var betweenSpaces = (int)Math.Pow(2, floor + 1) - 1;

                PrintWhitespaces(firstSpaces);

                var newNodes = new List<TNode>();
                foreach (var node in nodes)
                {
                    if (node != null)
                    {
                        _textWriter.Write(node.Value);
                        newNodes.Add(node.Left);
                        newNodes.Add(node.Right);
                    }
                    else
                    {
                        newNodes.Add(null);
                        newNodes.Add(null);
                        _textWriter.Write(" ");
                    }

                    PrintWhitespaces(betweenSpaces);
                }

                _textWriter.WriteLine();

                for (var i = 1; i <= endgeLines; i++)
                {
                    foreach (var t in nodes)
                    {
                        PrintWhitespaces(firstSpaces - i);
                        if (t == null)
                        {
                            PrintWhitespaces(endgeLines + endgeLines + i + 1);
                            continue;
                        }

                        if (t.Left != null)
                            _textWriter.Write("/");
                        else
                            PrintWhitespaces(1);

                        PrintWhitespaces(i + i - 1);

                        if (t.Right != null)
                            _textWriter.Write("\\");
                        else
                            PrintWhitespaces(1);

                        PrintWhitespaces(endgeLines + endgeLines - i);
                    }

                    _textWriter.WriteLine();
                }

                nodes = newNodes;
                level = level + 1;
            }
        }

        /// <summary>
        /// Prints specifed number of spaces
        /// </summary>
        /// <param name="count">Number of spaces</param>
        private void PrintWhitespaces(int count)
        {
            for (var i = 0; i < count; i++)
                _textWriter.Write(" ");
        }

        /// <summary>
        /// Gets the max level of node
        /// </summary>
        /// <param name="node">Node</param>
        /// <returns>The left or right level (whichever is highest) or 0 if node is null.</returns>
        private static int MaxLevel(TNode node)
        {
            if (node == null)
                return 0;

            return Math.Max(MaxLevel(node.Left), MaxLevel(node.Right)) + 1;
        }
    }
}
