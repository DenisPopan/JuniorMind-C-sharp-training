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
            var circularLinkedListNode = new CircularLinkedListNode<int>(16);
            circularLinkedList.Add(circularLinkedListNode);
            Assert.Equal(1, circularLinkedList.Count);
            circularLinkedListNode.Value = 22;
            circularLinkedList.Add(circularLinkedListNode);
            Assert.Equal(2, circularLinkedList.Count);
        }
    }
}
