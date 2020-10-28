using Xunit;
using IntegersArray;
using System;

namespace IntegersArrayFacts
{
    public class ReadOnlyListDecoratorFacts
    {
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
            var readOnlyList = new ReadOnlyListDecorator<int>(list1);
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
            var readOnlyList = new ReadOnlyListDecorator<int>(list1);
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
            var readOnlyList = new ReadOnlyListDecorator<int>(list1);
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
            var readOnlyList = new ReadOnlyListDecorator<int>(list1);
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
            var readOnlyList = new ReadOnlyListDecorator<int>(list1);
            Assert.Throws<NotSupportedException>(() => readOnlyList.Clear());
        }
    }
}
