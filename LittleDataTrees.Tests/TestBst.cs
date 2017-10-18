using LittleDataTrees.BST;
using LittleDataTrees.Exceptions;
using NUnit.Framework;

namespace LittleDataTrees.Tests
{
    [TestFixture]
    public class TestBst
    {
        public Tree<int> Tree;

        [SetUp]
        public void SetUp()
        {
            Tree = new Tree<int>();

            Tree.Add(6);
            Tree.Add(1);
            Tree.Add(8);
            Tree.Add(4);
            Tree.Add(3);
            Tree.Add(2);
            Tree.Add(10);
            Tree.Add(9);
            Tree.Add(5);
            Tree.Add(7);
        }

        /// <summary>
        /// Tests adding non existing element to tree.
        /// </summary>
        [Test]
        public void TestAdd()
        {
            const int toAdd = 11;
            Node<int> newNode = null;
            var expectedCount = Tree.Count + 1;

            Tree.Add(toAdd);

            Assert.True(Tree.Contains(toAdd));
            Assert.DoesNotThrow(() => newNode = Tree.Find(toAdd));
            Assert.AreEqual(toAdd, newNode.Value);
            Assert.AreEqual(expectedCount, Tree.Count);
        }

        /// <summary>
        /// Tests adding existing element to tree.
        /// </summary>
        [Test]
        public void TestAddExisting()
        {
            const int toAdd = 5;
            var countBeforeAdd = Tree.Count;

            Assert.True(Tree.Contains(toAdd));
            Assert.Throws<NodeAlreadyExistsException>(() => Tree.Add(toAdd));
            Assert.AreEqual(countBeforeAdd, Tree.Count);
        }
        
        /// <summary>
        /// Tests deleting node with 0 children.
        /// </summary>
        [Test]
        public void TestDeleteZeroChildren()
        {
            const int valueToDelete = 7;

            var nodeToDelete = Tree.Find(valueToDelete);
            var countBeforeDelete = Tree.Count;

            Assert.Null(nodeToDelete.Left);
            Assert.Null(nodeToDelete.Right);

            Tree.Delete(nodeToDelete.Value);

            Assert.Throws<NodeNotFoundException>(() => Tree.Find(valueToDelete));
            Assert.AreEqual(Tree.Count, countBeforeDelete - 1);
        }

        /// <summary>
        /// Tests deleting node with 1 child that's on the left.
        /// </summary>
        [Test]
        public void TestDeleteLeftChild()
        {
            const int valueToDelete = 10;

            var nodeToDelete = Tree.Find(valueToDelete);
            var countBeforeDelete = Tree.Count;

            Assert.NotNull(nodeToDelete.Left);
            Assert.Null(nodeToDelete.Right);

            Tree.Delete(nodeToDelete.Value);

            Assert.Throws<NodeNotFoundException>(() => Tree.Find(valueToDelete));
            Assert.AreEqual(Tree.Count, countBeforeDelete - 1);
        }

        /// <summary>
        /// Tests deleting node with 1 child that's on the right.
        /// </summary>
        [Test]
        public void TestDeleteRightChild()
        {
            const int valueToDelete = 1;

            var nodeToDelete = Tree.Find(valueToDelete);
            var countBeforeDelete = Tree.Count;

            Assert.Null(nodeToDelete.Left);
            Assert.NotNull(nodeToDelete.Right);

            Tree.Delete(nodeToDelete.Value);

            Assert.Throws<NodeNotFoundException>(() => Tree.Find(valueToDelete));
            Assert.AreEqual(Tree.Count, countBeforeDelete - 1);
        }

        /// <summary>
        /// Tests deleting node that has both left and right child.
        /// </summary>
        [Test]
        public void TestDeleteBothChildren()
        {
            const int valueToDelete = 6;
            var nodeToDelete = Tree.Find(valueToDelete);
            var countBeforeDelete = Tree.Count;

            Assert.NotNull(nodeToDelete.Left);
            Assert.NotNull(nodeToDelete.Right);

            Tree.Delete(nodeToDelete.Value);

            Assert.Throws<NodeNotFoundException>(() => Tree.Find(valueToDelete));
            Assert.AreEqual(Tree.Count, countBeforeDelete - 1);
        }

        /// <summary>
        /// Tests the height of the tree.
        /// </summary>
        [Test]
        public void TestTreeHeight()
        {
            const int expected = 5;
            var actual = Tree.Height;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the root node height.
        /// </summary>
        [Test]
        public void TestRootHeight()
        {
            const uint expected = 1;
            var actual = Tree.Root.Height;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the height of a node.
        /// </summary>
        [Test]
        public void TestNodeHeight()
        {
            const uint expected = 3;

            var actual = Tree.Find(7).Height;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests that tree contains a value.
        /// </summary>
        [Test]
        public void TestContains()
        {
            const int valueToFind = 6;

            Assert.True(Tree.Contains(valueToFind));
            Assert.DoesNotThrow(() => Tree.Find(valueToFind));
        }

        /// <summary>
        /// Tests that the tree doesn't contain a value.
        /// </summary>
        [Test]
        public void TestDoesntContains()
        {
            const int valueToFind = -1;

            Assert.False(Tree.Contains(valueToFind));
            Assert.Throws<NodeNotFoundException>(() => Tree.Find(valueToFind));
        }
    }
}
