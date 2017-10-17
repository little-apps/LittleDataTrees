using NUnit.Framework;
using Trees.AVL;
using Trees.Exceptions;

namespace LittleDataTrees.Tests
{
    [TestFixture]
    public class TestAvl
    {
        public Trees.AVL.Tree<int> Tree;

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
        /// Tests finding an item in tree.
        /// </summary>
        [Test]
        public void TestFind()
        {
            Node<int> actual = null;
            const int expected = 4;

            Assert.DoesNotThrow(() => actual = Tree.Find(4));
            
            Assert.AreEqual(expected, actual.Value);
        }

        /// <summary>
        /// Tests finding an item not in tree.
        /// </summary>
        [Test]
        public void TestFindNotFound()
        {
            Node<int> actual = null;
            
            Assert.Throws<NodeNotFoundException>(() => actual = Tree.Find(-1));

            Assert.Null(actual);
        }

        /// <summary>
        /// Tests deleting an item that exists in tree.
        /// </summary>
        [Test]
        public void TestDelete()
        {
            var beforeDeleteCount = Tree.Count;
            const int expected = 7;
            Node<int> actual = null;

            Assert.DoesNotThrow(() => Tree.Delete(expected));

            Assert.Less(Tree.Count, beforeDeleteCount);
            Assert.Throws<NodeNotFoundException>(() => actual = Tree.Find(expected));
            Assert.Null(actual);
        }

        /// <summary>
        /// Tests deleting item that isn't in tree.
        /// </summary>
        [Test]
        public void TestDeleteNotFound()
        {
            var beforeDeleteCount = Tree.Count;
            Assert.Throws<NodeNotFoundException>(() => Tree.Delete(-1));
            Assert.AreEqual(beforeDeleteCount, Tree.Count);
        }

        /// <summary>
        /// Tests that tree is not empty.
        /// </summary>
        [Test]
        public void TestNotEmpty()
        {
            Assert.Greater(Tree.Count, 0);
            Assert.False(Tree.IsEmpty);
        }

        /// <summary>
        /// Tests clearing tree.
        /// </summary>
        [Test]
        public void TestClear()
        {
            Tree.Clear();

            Assert.AreEqual(0, Tree.Count);
            Assert.True(Tree.IsEmpty);
        }

        /// <summary>
        /// Tests tree contains existing item.
        /// </summary>
        [Test]
        public void TestContains()
        {
            Assert.True(Tree.Contains(7));
        }

        /// <summary>
        /// Tests tree doesn't contain item.
        /// </summary>
        [Test]
        public void TestNotContains()
        {
            Assert.False(Tree.Contains(-1));
        }

        /// <summary>
        /// Tests adding item to tree.
        /// </summary>
        [Test]
        public void TestAdd()
        {
            const int addValue = -1;

            var beforeAddCount = Tree.Count;
            Node<int> addedNode;
            
            Assert.DoesNotThrow(() => addedNode = Tree.Add(addValue));
            Assert.Greater(Tree.Count, beforeAddCount);
            Assert.True(Tree.Contains(addValue));
        }

        /// <summary>
        /// Tests adding item already existing in tree.
        /// </summary>
        [Test]
        public void TestAddExisting()
        {
            const int addValue = 5;

            var beforeAddCount = Tree.Count;
            
            Assert.Throws<NodeAlreadyExistsException>(() => Tree.Add(addValue));

            Assert.AreEqual(beforeAddCount, Tree.Count);
        }

        /// <summary>
        /// Tests size of tree is incremented after add.
        /// </summary>
        [Test]
        public void TestAddSize()
        {
            var expected = Tree.Count + 1;

            Tree.Add(-1);

            var actual = Tree.Count;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the tree height.
        /// </summary>
        [Test]
        public void TestTreeHeight()
        {
            var expected = 4;
            var actual = Tree.Height;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the pre order enumeration
        /// </summary>
        [Test]
        public void TestPreOrder()
        {
            var expected = new[] {6, 3, 1, 2, 4, 5, 9, 8, 7, 10};
            var actualEnumerator = Tree.PreOrder;

            var i = 0;

            actualEnumerator.Reset();

            while (actualEnumerator.MoveNext())
            {
                Assert.AreEqual(expected[i], actualEnumerator.Current);

                i++;
            }

            Assert.AreEqual(expected.Length, i);
        }

        /// <summary>
        /// Tests the in order enumeration
        /// </summary>
        [Test]
        public void TestInOrder()
        {
            var expected = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var actualEnumerator = Tree.InOrder;

            var i = 0;

            actualEnumerator.Reset();

            while (actualEnumerator.MoveNext())
            {
                Assert.AreEqual(expected[i], actualEnumerator.Current);

                i++;
            }

            Assert.AreEqual(expected.Length, i);
        }

        /// <summary>
        /// Tests the post order enumeration
        /// </summary>
        [Test]
        public void TestPostOrder()
        {
            var expected = new[] {2, 1, 5, 4, 3, 7, 8, 10, 9, 6};
            var actualEnumerator = Tree.PostOrder;

            var i = 0;

            actualEnumerator.Reset();

            while (actualEnumerator.MoveNext())
            {
                Assert.AreEqual(expected[i], actualEnumerator.Current);

                i++;
            }

            Assert.AreEqual(expected.Length, i);
        }
    }
}
