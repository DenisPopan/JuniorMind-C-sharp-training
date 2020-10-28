using Xunit;
using IntegersArray;
using System;

namespace IntegersArrayFacts
{
    public class ListFacts
    {
        [Fact]

        public void IndexerThrowsExceptionWhenIndexIsInvalid()
        {
            var list1 = new List<int>();
            Assert.Empty(list1);
            list1.Add(15);
            list1.Add(22);
            list1.Add(10);
            list1.Add(16);
            list1.Add(28);
            Assert.Throws<ArgumentOutOfRangeException>(() => list1[16]);
        }

        [Fact]

        public void CopyToMethodShouldThrowArgumentNullExceptionWhenArrayIsNull()
        {
            var list1 = new List<int>();
            int[] array = null;
            list1.Add(15);
            list1.Add(22);

            Assert.Throws<ArgumentNullException>(() => list1.CopyTo(array, 0));
        }

        [Fact]

        public void CopyToMethodShouldThrowIndexOutOfRangeExceptionWhenArrayIndexIsBelowZero()
        {
            var list1 = new List<int>();
            int[] array = new int[2];
            list1.Add(15);
            list1.Add(22);

            Assert.Throws<ArgumentOutOfRangeException>(() => list1.CopyTo(array, -1));
        }

        [Fact]

        public void CopyToMethodShouldThrowArgumentExceptionWhenElementsCanNotBeCopied()
        {
            var list1 = new List<int>();
            int[] array = new int[2];
            list1.Add(15);
            list1.Add(22);

            Exception ex = Assert.Throws<ArgumentException>(() => list1.CopyTo(array, 1));
            Assert.Equal("number of elements in the instance is greater than the available space from arrayIndex to the end of the destination array", ex.Message);
        }

        [Fact]

        public void CopyToMethodShouldThrowArgumentOutOfRangeExceptionWhenArrayIndexIsBelowZero()
        {
            var list1 = new List<int>();
            int[] array = new int[2];
            list1.Add(15);
            list1.Add(22);

            Assert.Throws<ArgumentOutOfRangeException>(() => list1.CopyTo(array, -1));
        }

        [Fact]

        public void InsertMethodShouldThrowArgumentOutOfRangeExceptionWhenGivenInsertionIndexIsInvalid()
        {
            var list1 = new List<int>();
            list1.Add(15);
            list1.Add(22);
            list1.Add(346);

            Assert.Throws<ArgumentOutOfRangeException>(() => list1.Insert(3, 2350));
        }

        [Fact]

        public void RemoveAtMethodShouldThrowArgumentOutOfRangeExceptionWhenGivenElementIndexIsInvalid()
        {
            var list1 = new List<int>();
            list1.Add(15);
            list1.Add(22);
            list1.Add(346);

            Assert.Throws<ArgumentOutOfRangeException>(() => list1.RemoveAt(3));
        }


        [Fact]
        public void AddMethodWorksForAllBaseTypes()
        {
            var list1 = new List<int>();
            Assert.Empty(list1);
            list1.Add(15);
            list1.Add(22);
            list1.Add(10);
            list1.Add(16);
            list1.Add(28);
            Assert.Equal(5, list1.Count);
            Assert.Equal(15, list1[0]);
            Assert.Equal(22, list1[1]);
            Assert.Equal(10, list1[2]);
            Assert.Equal(16, list1[3]);
            Assert.Equal(28, list1[4]);

            var list2 = new List<string>();
            list2.Add("hey");
            list2.Add("house");
            list2.Add("party");
            list2.Add("sounds");
            list2.Add("great");
            Assert.Equal(5, list2.Count);
            Assert.Equal("hey", list2[0]);
            Assert.Equal("house", list2[1]);
            Assert.Equal("party", list2[2]);
            Assert.Equal("sounds", list2[3]);
            Assert.Equal("great", list2[4]);

            var list3 = new List<bool>();
            list3.Add(true);
            list3.Add(false);
            Assert.True(list3[0]);
            Assert.False(list3[1]);

            var list4 = new List<double>();
            list4.Add(16.2);
            list4.Add(36e16);
            Assert.Equal(16.2, list4[0]);
            Assert.Equal(36e16, list4[1]);

            var list5 = new List<char>
            {
                'e',
                'g'
            };
            Assert.Equal('e', list5[0]);
            Assert.Equal('g', list5[1]);
        }

        [Fact]
        public void ContainsMethodWorksForAllBaseTypes()
        {
            var list1 = new List<int>();
            Assert.Equal(0, list1.Count);
            list1.Add(15);
            Assert.True(list1.Contains(15));

            var list2 = new List<string>();
            Assert.Equal(0, list2.Count);
            list2.Add("hey");
            Assert.True(list2.Contains("hey"));

            var list3 = new List<bool>();
            Assert.Equal(0, list3.Count);
            list3.Add(true);
            Assert.True(list3.Contains(true));

            var list4 = new List<char>();
            Assert.Equal(0, list4.Count);
            list4.Add('e');
            Assert.True(list4.Contains('e'));

            var list5 = new List<double>();
            Assert.Equal(0, list5.Count);
            list5.Add(15.3e4);
            Assert.True(list5.Contains(15.3e4));

        }

        [Fact]
        public void IndexOfMethodWorksForAllBaseTypes()
        {
            var list1 = new List<int>();
            Assert.Equal(0, list1.Count);
            list1.Add(15);
            Assert.Equal(0, list1.IndexOf(15));

            var list2 = new List<string>();
            Assert.Equal(0, list2.Count);
            list2.Add("hey");
            Assert.Equal(0, list2.IndexOf("hey"));

            var list3 = new List<bool>();
            Assert.Equal(0, list3.Count);
            list3.Add(true);
            Assert.Equal(0, list3.IndexOf(true));

            var list4 = new List<char>();
            Assert.Equal(0, list4.Count);
            list4.Add('e');
            Assert.Equal(0, list4.IndexOf('e'));

            var list5 = new List<double>();
            Assert.Equal(0, list5.Count);
            list5.Add(15.3e4);
            Assert.Equal(0, list5.IndexOf(15.3e4));
        }

        [Fact]
        public void InsertMethodWorksForAllBaseTypes()
        {
            var list1 = new List<int>();
            Assert.Equal(0, list1.Count);
            list1.Add(15);
            list1.Insert(0, 12);
            list1.Insert(0, 16);
            Assert.Equal(16, list1[0]);
            Assert.Equal(12, list1[1]);

            var list2 = new List<string>();
            Assert.Equal(0, list2.Count);
            list2.Add("hey");
            list2.Insert(0, "after");
            list2.Insert(0, "cyborg");
            Assert.Equal("cyborg", list2[0]);
            Assert.Equal("after", list2[1]);

            var list3 = new List<bool>();
            Assert.Equal(0, list3.Count);
            list3.Add(true);
            list3.Insert(0, false);
            list3.Insert(0, true);
            Assert.Equal(true, list3[0]);
            Assert.Equal(false, list3[1]);

            var list4 = new List<char>();
            Assert.Equal(0, list4.Count);
            list4.Add('e');
            list4.Insert(0, 'a');
            list4.Insert(0, 'c');
            Assert.Equal('c', list4[0]);
            Assert.Equal('a', list4[1]);

            var list5 = new List<double>();
            Assert.Equal(0, list5.Count);
            list5.Add(15.3e4);
            list5.Insert(0, 11.2);
            list5.Insert(0, 124.21);
            Assert.Equal(124.21, list5[0]);
            Assert.Equal(11.2, list5[1]);
        }

        [Fact]
        public void ClearMethodWorksForAllBaseTypes()
        {
            var list1 = new List<int>();
            Assert.Equal(0, list1.Count);
            list1.Add(15);
            list1.Add(22);
            list1.Add(10);
            list1.Add(346);
            list1.Add(734);
            list1.Insert(2, 745);
            list1.Insert(4, 234);
            list1.Insert(1, 61);
            list1.Clear();
            Assert.Equal(0, list1.Count);
        }

        [Fact]
        public void IsReadOnlyMethodReturnsFalse()
        {
            var list1 = new List<int>();
            list1.Add(15);
            list1.Add(22);
            list1.Add(10);
            Assert.False(list1.IsReadOnly);
        }

        [Fact]
        public void CopyToMethodWorksAccordingly()
        {
            int[] array = new int[4];
            var list1 = new List<int>();
            list1.Add(15);
            list1.Add(22);
            list1.Add(10);
            list1.Add(235);
            list1.CopyTo(array, 0);
            Assert.Equal(15, array[0]);
            Assert.Equal(22, array[1]);
            Assert.Equal(10, array[2]);
            Assert.Equal(235, array[3]);
        }

        [Fact]
        public void RemoveMethodWorksForAllBaseTypes()
        {
            var list1 = new List<int>();
            Assert.Equal(0, list1.Count);
            list1.Add(15);
            list1.Add(22);
            list1.Add(16);
            list1.Add(69);
            list1.Add(47);
            list1.Insert(2, 37);
            list1.Insert(4, 7457);
            list1.Insert(1, 66);
            list1.Remove(69);
            list1.Remove(7457);

            Assert.Equal(15, list1[0]);
            Assert.Equal(66, list1[1]);
            Assert.Equal(22, list1[2]);
            Assert.Equal(37, list1[3]);
            Assert.Equal(16, list1[4]);
            Assert.Equal(47, list1[5]);

        }

        [Fact]
        public void RemoveAtMethodWorksForAllBaseTypes()
        {
            var list1 = new List<int>();
            Assert.Equal(0, list1.Count);
            list1.Add(15);
            list1.Add(22);
            list1.Add(16);
            list1.Add(69);
            list1.Add(47);
            list1.Insert(2, 37);
            list1.Insert(4, 7457);
            list1.Insert(1, 66);
            list1.RemoveAt(4);
            list1.RemoveAt(1);


            Assert.Equal(15, list1[0]);
            Assert.Equal(22, list1[1]);
            Assert.Equal(37, list1[2]);
            Assert.Equal(7457, list1[3]);
            Assert.Equal(69, list1[4]);
            Assert.Equal(47, list1[5]);
        }
    }
}
