using Xunit;
using IntegersArray;
using System;

namespace IntegersArrayFacts
{
    public class ReadOnlyListFacts
    {
        [Fact]

        public void IndexerReturnsCorrectListElement()
        {
            var list1 = new List<int>();
            Assert.Empty(list1);
            list1.Add(15);
            list1.Add(22);
            list1.Add(10);
            list1.Add(16);
            list1.Add(28);
            var readOnlyList = new ReadOnlyList<int>(list1);
            Assert.Equal(16, readOnlyList[3]);
        }

        [Fact]

        public void IndexerThrowsNotSupportedExceptionWhenListIsReadOnly()
        {
            var list1 = new List<int>();
            Assert.Empty(list1);
            list1.Add(15);
            list1.Add(22);
            list1.Add(10);
            list1.Add(16);
            list1.Add(28);
            var readOnlyList = new ReadOnlyList<int>(list1);
            Assert.Throws<NotSupportedException>(() => readOnlyList[2] = 124);
        }

        [Fact]

        public void AddMethodThrowsNotSupportedExceptionWhenListIsReadOnly()
        {
            var list1 = new List<int>();
            Assert.Empty(list1);
            list1.Add(15);
            list1.Add(22);
            list1.Add(10);
            list1.Add(16);
            list1.Add(28);
            var readOnlyList = new ReadOnlyList<int>(list1);
            Assert.Throws<NotSupportedException>(() => readOnlyList.Add(1234));
        }

        [Fact]

        public void InsertMethodThrowsNotSupportedExceptionWhenListIsReadOnly()
        {
            var list1 = new List<int>();
            Assert.Empty(list1);
            list1.Add(15);
            list1.Add(22);
            list1.Add(10);
            list1.Add(16);
            list1.Add(28);
            var readOnlyList = new ReadOnlyList<int>(list1);
            Assert.Throws<NotSupportedException>(() => readOnlyList.Insert(0, 1234));
        }

        [Fact]

        public void RemoveAndRemoveAtMethodsThrowsNotSupportedExceptionWhenListIsReadOnly()
        {
            var list1 = new List<int>();
            Assert.Empty(list1);
            list1.Add(15);
            list1.Add(22);
            list1.Add(10);
            list1.Add(16);
            list1.Add(28);
            var readOnlyList = new ReadOnlyList<int>(list1);
            Assert.Throws<NotSupportedException>(() => readOnlyList.Remove(10));
            Assert.Throws<NotSupportedException>(() => readOnlyList.RemoveAt(0));
        }

        [Fact]

        public void ClearMethodThrowsNotSupportedExceptionWhenListIsReadOnly()
        {
            var list1 = new List<int>();
            Assert.Empty(list1);
            list1.Add(15);
            list1.Add(22);
            list1.Add(10);
            list1.Add(16);
            list1.Add(28);
            var readOnlyList = new ReadOnlyList<int>(list1);
            Assert.Throws<NotSupportedException>(() => readOnlyList.Clear());
        }

        [Fact]

        public void OtherMethodsShouldWorkTheSame()
        {
            var list1 = new List<int>();
            Assert.Empty(list1);
            list1.Add(15);
            list1.Add(22);
            list1.Add(10);
            list1.Add(16);
            list1.Add(28);
            var readOnlyList = new ReadOnlyList<int>(list1);
            Assert.Equal(5, readOnlyList.Count);
            Assert.Equal(16, readOnlyList[3]);
            Assert.False(readOnlyList.Contains(62));
            Assert.Equal(4, readOnlyList.IndexOf(28));
            var array = new int[5];
            readOnlyList.CopyTo(array, 0);
            Assert.Equal(15, array[0]);
            Assert.Equal(28, array[4]);
        }
    }
}
