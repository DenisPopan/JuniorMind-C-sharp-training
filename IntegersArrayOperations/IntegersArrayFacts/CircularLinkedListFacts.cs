﻿using Xunit;
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
    }
}
