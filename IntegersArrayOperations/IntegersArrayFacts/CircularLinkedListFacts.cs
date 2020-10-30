using Xunit;
using IntegersArray;
using System;

namespace IntegersArrayFacts
{
    public class CircularLinkedListFacts
    {
        [Fact]

        public void AddMethodShouldAddElementAtTheEndOfThelist()
        {
            var circularLinkedList = new CircularLinkedList<int>();
            var circularLinkedListNode1 = new CircularLinkedListNode<int>(16);
            circularLinkedList.Add(circularLinkedListNode1);
            Assert.Equal(1, circularLinkedList.Count);
            var circularLinkedListNode2 = new CircularLinkedListNode<int>(22);
            circularLinkedList.Add(circularLinkedListNode2);
            Assert.Equal(2, circularLinkedList.Count);
        }

        [Fact]

        public void AddMethodShouldThrowAnExceptionWhenAdeedItemIsNull()
        {
            var circularLinkedList = new CircularLinkedList<int>();
            CircularLinkedListNode<int> circularLinkedListNode1 = null;
            Assert.Throws<ArgumentNullException>(() => circularLinkedList.Add(circularLinkedListNode1));
        }

        [Fact]

        public void FirstPropertyShouldReturnFirstNodeInTheList()
        {
            var circularLinkedList = new CircularLinkedList<int>();

            var circularLinkedListNode1 = new CircularLinkedListNode<int>(16);
            var circularLinkedListNode2 = new CircularLinkedListNode<int>(22);

            circularLinkedList.Add(circularLinkedListNode1);
            circularLinkedList.Add(circularLinkedListNode2);

            Assert.Equal(16, circularLinkedList.First.Value);
        }

        [Fact]

        public void FirstPropertyShouldThrowAnExceptionWhenListIsEmpty()
        {
            var circularLinkedList = new CircularLinkedList<int>();

            Assert.Throws<ArgumentException>(() => circularLinkedList.First);
        }

        [Fact]

        public void LastPropertyShouldReturnLastNodeInTheList()
        {
            var circularLinkedList = new CircularLinkedList<int>();

            var circularLinkedListNode1 = new CircularLinkedListNode<int>(16);
            var circularLinkedListNode2 = new CircularLinkedListNode<int>(22);

            circularLinkedList.Add(circularLinkedListNode1);
            circularLinkedList.Add(circularLinkedListNode2);

            Assert.Equal(22, circularLinkedList.Last.Value);
        }

        [Fact]

        public void LastPropertyShouldThrowAnExceptionWhenListIsEmpty()
        {
            var circularLinkedList = new CircularLinkedList<int>();

            Assert.Throws<ArgumentException>(() => circularLinkedList.Last);
        }

        [Fact]

        public void ClearMethodShouldDeleteAllNodesFromTheList()
        {
            var circularLinkedList = new CircularLinkedList<int>();

            var circularLinkedListNode1 = new CircularLinkedListNode<int>(16);
            var circularLinkedListNode2 = new CircularLinkedListNode<int>(22);
            var circularLinkedListNode3 = new CircularLinkedListNode<int>(474);
            var circularLinkedListNode4 = new CircularLinkedListNode<int>(235356);

            circularLinkedList.Add(circularLinkedListNode1);
            circularLinkedList.Add(circularLinkedListNode2);
            circularLinkedList.Add(circularLinkedListNode3);
            circularLinkedList.Add(circularLinkedListNode4);

            Assert.Equal(4, circularLinkedList.Count);
            Assert.Equal(235356, circularLinkedList.Last.Value);

            circularLinkedList.Clear();

            Assert.Equal(0, circularLinkedList.Count);
            Assert.Throws<ArgumentException>(() => circularLinkedList.First);
            Assert.Throws<ArgumentException>(() => circularLinkedList.Last);
        }
    }
}
