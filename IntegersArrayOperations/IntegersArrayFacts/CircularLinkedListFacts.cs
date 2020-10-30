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

        public void FirstPropertyShouldReturnFirstNodeInTheList()
        {
            var circularLinkedList = new CircularLinkedList<int>();

            var circularLinkedListNode1 = new CircularLinkedListNode<int>(16);
            var circularLinkedListNode2 = new CircularLinkedListNode<int>(22);

            circularLinkedList.Add(circularLinkedListNode1);
            circularLinkedList.Add(circularLinkedListNode2);

            Assert.Equal(16, circularLinkedList.First.Value);
        }
    }
}
