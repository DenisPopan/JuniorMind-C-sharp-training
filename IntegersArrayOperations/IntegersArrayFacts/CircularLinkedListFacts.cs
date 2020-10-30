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
    }
}
