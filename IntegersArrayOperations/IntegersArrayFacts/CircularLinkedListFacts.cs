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
            circularLinkedList.Add(16);
            Assert.Equal(1, circularLinkedList.Count);
            circularLinkedList.Add(22);
            Assert.Equal(2, circularLinkedList.Count);
        }

        [Fact]

        public void FirstPropertyShouldReturnFirstNodeInTheList()
        {
            var circularLinkedList = new CircularLinkedList<int>();

            circularLinkedList.Add(16);
            circularLinkedList.Add(22);

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

            circularLinkedList.Add(16);
            circularLinkedList.Add(22);

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

            circularLinkedList.Add(16);
            circularLinkedList.Add(22);
            circularLinkedList.Add(474);
            circularLinkedList.Add(235356);

            Assert.Equal(4, circularLinkedList.Count);
            Assert.Equal(235356, circularLinkedList.Last.Value);

            circularLinkedList.Clear();

            Assert.Equal(0, circularLinkedList.Count);
            Assert.Throws<ArgumentException>(() => circularLinkedList.First);
            Assert.Throws<ArgumentException>(() => circularLinkedList.Last);
        }

        [Fact]

        public void ContainsMethodShouldReturnIfTheListContainsANodeWithTheSpecifiedValue()
        {
            var circularLinkedList = new CircularLinkedList<int>();

            circularLinkedList.Add(16);
            circularLinkedList.Add(22);
            circularLinkedList.Add(474);
            circularLinkedList.Add(235356);

            Assert.Equal(4, circularLinkedList.Count);
            Assert.Equal(235356, circularLinkedList.Last.Value);

            Assert.True(circularLinkedList.Contains(474));
            Assert.True(circularLinkedList.Contains(235356));
            Assert.False(circularLinkedList.Contains(12));
        }

        [Fact]

        public void RemoveMethodShouldRemoveFirstOccurenceNodeWithGivenValue()
        {
            var circularLinkedList = new CircularLinkedList<int>();

            circularLinkedList.Add(16);
            circularLinkedList.Add(22);
            circularLinkedList.Add(9234);
            circularLinkedList.Add(474);
            circularLinkedList.Add(235356);

            circularLinkedList.Remove(235356);

            Assert.Equal(4, circularLinkedList.Count);
            Assert.Equal(474, circularLinkedList.Last.Value);

            Assert.True(circularLinkedList.Remove(16));

            Assert.Equal(3, circularLinkedList.Count);
            Assert.Equal(22, circularLinkedList.First.Value);

            circularLinkedList.Remove(9234);

            Assert.Equal(2, circularLinkedList.Count);
            Assert.Equal(474, circularLinkedList.First.Next.Value);

            Assert.False(circularLinkedList.Remove(34634));

            Assert.Equal(2, circularLinkedList.Count);

            var circularLinkedList2 = new CircularLinkedList<int>();

            circularLinkedList2.Add(16);
            circularLinkedList2.Add(22);
            circularLinkedList2.Add(22);

            circularLinkedList2.Remove(22);
            Assert.Equal(16, circularLinkedList2.First.Value);
            Assert.Equal(22, circularLinkedList2.Last.Value);
            Assert.Equal(2, circularLinkedList2.Count);
        }

        [Fact]

        public void CopyToMethodShouldCopyNodesValuesToAGivenArray()
        {
            var circularLinkedList = new CircularLinkedList<int>();

            int[] array = new int[4];

            circularLinkedList.Add(16);
            circularLinkedList.Add(22);
            circularLinkedList.Add(474);
            circularLinkedList.Add(235356);

            circularLinkedList.CopyTo(array, 0);
            Assert.Equal(16, array[0]);
            Assert.Equal(235356, array[3]);
            Assert.Equal(4, array.Length);
        }

        [Fact]

        public void CopyToMethodShouldThrowAppropriateExceptions()
        {
            var circularLinkedList = new CircularLinkedList<int>();

            int[] array1 = null;
            int[] array2 = new int[4];

            circularLinkedList.Add(16);
            circularLinkedList.Add(22);
            circularLinkedList.Add(474);
            circularLinkedList.Add(235356);

            Assert.Throws<ArgumentNullException>(() => circularLinkedList.CopyTo(array1, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => circularLinkedList.CopyTo(array2, -1));
            Assert.Throws<ArgumentException>(() => circularLinkedList.CopyTo(array2, 2));
        }

        [Fact]

        public void AddLastMethodShouldAddNodeAtTheEndOfThelist()
        {
            var circularLinkedList = new CircularLinkedList<int>();
            var circularLinkedListNode1 = new CircularLinkedListNode<int>();
            var circularLinkedListNode2 = new CircularLinkedListNode<int>(16);

            circularLinkedList.AddLast(circularLinkedListNode1);
            Assert.Equal(1, circularLinkedList.Count);
            Assert.Equal(circularLinkedList.First, circularLinkedListNode1);

            circularLinkedList.AddLast(circularLinkedListNode2);
            Assert.Equal(2, circularLinkedList.Count);
            Assert.Equal(circularLinkedList.Last, circularLinkedListNode2);
        }

        [Fact]

        public void AddAfterMethodShouldAddNewNodeAfterExistingOne()
        {
            var circularLinkedList = new CircularLinkedList<int>();
            var circularLinkedListNode1 = new CircularLinkedListNode<int>(16);
            var circularLinkedListNode2 = new CircularLinkedListNode<int>(22);
            var circularLinkedListNode3 = new CircularLinkedListNode<int>(474);
            var circularLinkedListNode4 = new CircularLinkedListNode<int>(235356);
            var circularLinkedListNode5 = new CircularLinkedListNode<int>(82);
            var circularLinkedListNode6 = new CircularLinkedListNode<int>(33);

            circularLinkedList.AddLast(circularLinkedListNode1);
            circularLinkedList.AddLast(circularLinkedListNode2);
            circularLinkedList.AddLast(circularLinkedListNode3);
            circularLinkedList.AddLast(circularLinkedListNode4);

            circularLinkedList.AddAfter(circularLinkedListNode1, circularLinkedListNode5);
            Assert.Equal(circularLinkedListNode5, circularLinkedListNode1.Next);
            Assert.Equal(circularLinkedListNode5, circularLinkedListNode2.Previous);
            Assert.Equal(circularLinkedListNode1, circularLinkedListNode5.Previous);
            Assert.Equal(circularLinkedListNode2, circularLinkedListNode5.Next);
            Assert.Equal(circularLinkedListNode2, circularLinkedListNode3.Previous);

            circularLinkedList.AddAfter(circularLinkedListNode4, circularLinkedListNode6);
            Assert.Equal(circularLinkedListNode6, circularLinkedListNode4.Next);
            Assert.Equal(0, circularLinkedListNode6.Next.Value);
            Assert.Equal(circularLinkedListNode4, circularLinkedListNode6.Previous);
        }

        [Fact]

        public void AddAfterMethodShouldAddNewNodeWithGivenValueAfterExistingOne()
        {
            var circularLinkedList = new CircularLinkedList<int>();
            var circularLinkedListNode1 = new CircularLinkedListNode<int>(16);
            var circularLinkedListNode2 = new CircularLinkedListNode<int>(22);
            var circularLinkedListNode3 = new CircularLinkedListNode<int>(474);
            var circularLinkedListNode4 = new CircularLinkedListNode<int>(235356);

            circularLinkedList.AddLast(circularLinkedListNode1);
            circularLinkedList.AddLast(circularLinkedListNode2);
            circularLinkedList.AddLast(circularLinkedListNode3);
            circularLinkedList.AddLast(circularLinkedListNode4);

            circularLinkedList.AddAfter(circularLinkedListNode1, 82);
            Assert.Equal(82, circularLinkedListNode1.Next.Value);
            Assert.Equal(82, circularLinkedListNode2.Previous.Value);
            Assert.Equal(circularLinkedListNode2, circularLinkedListNode3.Previous);

            circularLinkedList.AddAfter(circularLinkedListNode4, 33);
            Assert.Equal(33, circularLinkedListNode4.Next.Value);
        }

        [Fact]

        public void AddBeforeMethodShouldAddNewNodeBeforeExistingOne()
        {
            var circularLinkedList = new CircularLinkedList<int>();
            var circularLinkedListNode1 = new CircularLinkedListNode<int>(16);
            var circularLinkedListNode2 = new CircularLinkedListNode<int>(22);
            var circularLinkedListNode3 = new CircularLinkedListNode<int>(474);
            var circularLinkedListNode4 = new CircularLinkedListNode<int>(235356);
            var circularLinkedListNode5 = new CircularLinkedListNode<int>(82);
            var circularLinkedListNode6 = new CircularLinkedListNode<int>(33);

            circularLinkedList.AddLast(circularLinkedListNode1);
            circularLinkedList.AddLast(circularLinkedListNode2);
            circularLinkedList.AddLast(circularLinkedListNode3);
            circularLinkedList.AddLast(circularLinkedListNode4);

            circularLinkedList.AddBefore(circularLinkedListNode2, circularLinkedListNode5);
            Assert.Equal(circularLinkedListNode5, circularLinkedListNode2.Previous);
            Assert.Equal(circularLinkedListNode5, circularLinkedListNode1.Next);
            Assert.Equal(circularLinkedListNode1, circularLinkedListNode5.Previous);
            Assert.Equal(circularLinkedListNode2, circularLinkedListNode5.Next);
            Assert.Equal(circularLinkedListNode2, circularLinkedListNode3.Previous);

            circularLinkedList.AddBefore(circularLinkedListNode1, circularLinkedListNode6);
            Assert.Equal(circularLinkedListNode6, circularLinkedListNode1.Previous);
            Assert.Equal(0, circularLinkedListNode6.Previous.Value);
            Assert.Equal(circularLinkedListNode1, circularLinkedListNode6.Next);
        }

        [Fact]

        public void AddBeforeMethodShouldAddNewNodeWithGivenValueBeforeExistingOne()
        {
            var circularLinkedList = new CircularLinkedList<int>();
            var circularLinkedListNode1 = new CircularLinkedListNode<int>(16);
            var circularLinkedListNode2 = new CircularLinkedListNode<int>(22);
            var circularLinkedListNode3 = new CircularLinkedListNode<int>(474);
            var circularLinkedListNode4 = new CircularLinkedListNode<int>(235356);

            circularLinkedList.AddLast(circularLinkedListNode1);
            circularLinkedList.AddLast(circularLinkedListNode2);
            circularLinkedList.AddLast(circularLinkedListNode3);
            circularLinkedList.AddLast(circularLinkedListNode4);

            circularLinkedList.AddBefore(circularLinkedListNode2, 82);
            Assert.Equal(82, circularLinkedListNode2.Previous.Value);
            Assert.Equal(82, circularLinkedListNode1.Next.Value);

            circularLinkedList.AddBefore(circularLinkedListNode1, 33);
            Assert.Equal(33, circularLinkedListNode1.Previous.Value);
            Assert.Equal(0, circularLinkedListNode1.Previous.Previous.Value);
        }

        [Fact]

        public void AddFirstMethodShouldAddNewNodeAtTheStartOfTheList()
        {
            var circularLinkedList = new CircularLinkedList<int>();
            var circularLinkedListNode1 = new CircularLinkedListNode<int>(16);
            var circularLinkedListNode2 = new CircularLinkedListNode<int>(22);
            var circularLinkedListNode3 = new CircularLinkedListNode<int>(474);
            var circularLinkedListNode4 = new CircularLinkedListNode<int>(235356);

            circularLinkedList.AddFirst(circularLinkedListNode1);
            circularLinkedList.AddFirst(circularLinkedListNode2);
            circularLinkedList.AddFirst(circularLinkedListNode3);
            circularLinkedList.AddFirst(circularLinkedListNode4);

            Assert.Equal(0, circularLinkedListNode4.Previous.Value);
            Assert.Equal(circularLinkedListNode3, circularLinkedListNode4.Next);
            Assert.Equal(circularLinkedListNode4, circularLinkedListNode3.Previous);
            Assert.Equal(circularLinkedListNode3, circularLinkedListNode2.Previous);
            Assert.Equal(circularLinkedListNode2, circularLinkedListNode1.Previous);
        }

        [Fact]

        public void AddFirstMethodShouldAddNewNodeWithGivenValueAtTheStartOfTheList()
        {
            var circularLinkedList = new CircularLinkedList<int>();
            var circularLinkedListNode1 = new CircularLinkedListNode<int>(16);

            circularLinkedList.AddFirst(circularLinkedListNode1);
            circularLinkedList.AddFirst(22);
            circularLinkedList.AddFirst(474);
            circularLinkedList.AddFirst(246);

            Assert.Equal(0, circularLinkedListNode1.Previous.Previous.Previous.Previous.Value);
            Assert.Equal(246, circularLinkedListNode1.Previous.Previous.Previous.Value);
            Assert.Equal(474, circularLinkedListNode1.Previous.Previous.Value);
            Assert.Equal(22, circularLinkedListNode1.Previous.Value);
        }

        [Fact]

        public void FindMethodShouldReturnFirstNodeWithGivenValue()
        {
            var circularLinkedList = new CircularLinkedList<int>();
            var circularLinkedListNode1 = new CircularLinkedListNode<int>(16);
            var circularLinkedListNode2 = new CircularLinkedListNode<int>(22);
            var circularLinkedListNode3 = new CircularLinkedListNode<int>(474);
            var circularLinkedListNode4 = new CircularLinkedListNode<int>(235356);
            var circularLinkedListNode5 = new CircularLinkedListNode<int>(474);

            circularLinkedList.AddLast(circularLinkedListNode1);
            circularLinkedList.AddLast(circularLinkedListNode2);
            circularLinkedList.AddLast(circularLinkedListNode3);
            circularLinkedList.AddLast(circularLinkedListNode4);
            circularLinkedList.AddLast(circularLinkedListNode5);

            Assert.Equal(circularLinkedListNode2, circularLinkedList.Find(474).Previous);
            Assert.Equal(circularLinkedListNode4, circularLinkedList.Find(474).Next);
            Assert.Null(circularLinkedList.Find(4));
        }

        [Fact]

        public void FindLastMethodShouldReturnLastNodeWithGivenValue()
        {
            var circularLinkedList = new CircularLinkedList<int>();
            var circularLinkedListNode1 = new CircularLinkedListNode<int>(16);
            var circularLinkedListNode2 = new CircularLinkedListNode<int>(22);
            var circularLinkedListNode3 = new CircularLinkedListNode<int>(474);
            var circularLinkedListNode4 = new CircularLinkedListNode<int>(235356);
            var circularLinkedListNode5 = new CircularLinkedListNode<int>(474);

            circularLinkedList.AddLast(circularLinkedListNode1);
            circularLinkedList.AddLast(circularLinkedListNode2);
            circularLinkedList.AddLast(circularLinkedListNode3);
            circularLinkedList.AddLast(circularLinkedListNode4);
            circularLinkedList.AddLast(circularLinkedListNode5);

            Assert.Equal(circularLinkedListNode4, circularLinkedList.FindLast(474).Previous);
            Assert.Equal(0, circularLinkedList.FindLast(474).Next.Value);
            Assert.Null(circularLinkedList.FindLast(4));
        }

        [Fact]

        public void RemoveMethodShouldRemoveGivenNode()
        {
            var circularLinkedList = new CircularLinkedList<int>();
            var circularLinkedListNode1 = new CircularLinkedListNode<int>(16);
            var circularLinkedListNode2 = new CircularLinkedListNode<int>(22);
            var circularLinkedListNode3 = new CircularLinkedListNode<int>(474);
            var circularLinkedListNode4 = new CircularLinkedListNode<int>(235356);
            var circularLinkedListNode5 = new CircularLinkedListNode<int>(474);
            var circularLinkedListNode6 = new CircularLinkedListNode<int>(88);

            circularLinkedList.AddLast(circularLinkedListNode1);
            circularLinkedList.AddLast(circularLinkedListNode2);
            circularLinkedList.AddLast(circularLinkedListNode3);
            circularLinkedList.AddLast(circularLinkedListNode4);
            circularLinkedList.AddLast(circularLinkedListNode5);

            Assert.True(circularLinkedList.Remove(circularLinkedListNode3));
            Assert.Equal(circularLinkedListNode4, circularLinkedListNode2.Next);
            Assert.Equal(circularLinkedListNode2, circularLinkedListNode4.Previous);
            Assert.Throws<InvalidOperationException>(() => circularLinkedList.Remove(circularLinkedListNode6));
        }

        [Fact]

        public void RemoveFirstMethodShouldRemoveFirstNode()
        {
            var circularLinkedList = new CircularLinkedList<int>();
            var circularLinkedListNode1 = new CircularLinkedListNode<int>(16);
            var circularLinkedListNode2 = new CircularLinkedListNode<int>(22);
            var circularLinkedListNode3 = new CircularLinkedListNode<int>(474);
            var circularLinkedListNode4 = new CircularLinkedListNode<int>(235356);
            var circularLinkedListNode5 = new CircularLinkedListNode<int>(474);

            circularLinkedList.AddLast(circularLinkedListNode1);
            circularLinkedList.AddLast(circularLinkedListNode2);
            circularLinkedList.AddLast(circularLinkedListNode3);
            circularLinkedList.AddLast(circularLinkedListNode4);
            circularLinkedList.AddLast(circularLinkedListNode5);

            Assert.Equal(circularLinkedList.First, circularLinkedListNode1);
            circularLinkedList.RemoveFirst();
            Assert.Equal(circularLinkedList.First, circularLinkedListNode2);
        }

        [Fact]

        public void RemoveLastMethodShouldRemoveLastNode()
        {
            var circularLinkedList = new CircularLinkedList<int>();
            var circularLinkedListNode1 = new CircularLinkedListNode<int>(16);
            var circularLinkedListNode2 = new CircularLinkedListNode<int>(22);
            var circularLinkedListNode3 = new CircularLinkedListNode<int>(474);
            var circularLinkedListNode4 = new CircularLinkedListNode<int>(235356);
            var circularLinkedListNode5 = new CircularLinkedListNode<int>(474);

            circularLinkedList.AddLast(circularLinkedListNode1);
            circularLinkedList.AddLast(circularLinkedListNode2);
            circularLinkedList.AddLast(circularLinkedListNode3);
            circularLinkedList.AddLast(circularLinkedListNode4);
            circularLinkedList.AddLast(circularLinkedListNode5);

            Assert.Equal(circularLinkedList.Last, circularLinkedListNode5);
            circularLinkedList.RemoveLast();
            Assert.Equal(circularLinkedList.Last, circularLinkedListNode4);
        }

    }
}
