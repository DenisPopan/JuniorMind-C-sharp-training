using Xunit;
using IntegersArray;

namespace IntegersArrayFacts
{
    public class SortedIntArrayFacts
    {
        [Fact]
        public void AddMethodShouldSortArrayAfterEveryAddition()
        {
            SortedIntArray sortedArray = new SortedIntArray();
            IntArray array = new IntArray();
            sortedArray.Add(9);
            sortedArray.Add(7);
            sortedArray.Add(5);
            Assert.Equal(5, sortedArray[0]);
            Assert.Equal(7, sortedArray[1]);
            Assert.Equal(9, sortedArray[2]);
            Assert.Equal(0, array.Count);
        }

        [Fact]
        public void SetterShouldSetElementOnlyIfArrayRemainsSorted()
        {
            SortedIntArray sortedArray = new SortedIntArray();
            sortedArray.Add(5);
            sortedArray.Add(7);
            sortedArray.Add(9);
            sortedArray[0] = 10;
            Assert.Equal(5, sortedArray[0]);
            Assert.Equal(7, sortedArray[1]);
            Assert.Equal(9, sortedArray[2]);
            sortedArray[0] = 2;
            Assert.Equal(2, sortedArray[0]);
            Assert.Equal(7, sortedArray[1]);
            Assert.Equal(9, sortedArray[2]);
            sortedArray[2] = 1;
            Assert.Equal(2, sortedArray[0]);
            Assert.Equal(7, sortedArray[1]);
            Assert.Equal(9, sortedArray[2]);
            sortedArray[2] = 14;
            Assert.Equal(2, sortedArray[0]);
            Assert.Equal(7, sortedArray[1]);
            Assert.Equal(14, sortedArray[2]);
            sortedArray[1] = 20;
            Assert.Equal(2, sortedArray[0]);
            Assert.Equal(7, sortedArray[1]);
            Assert.Equal(14, sortedArray[2]);
            sortedArray[1] = 8;
            Assert.Equal(2, sortedArray[0]);
            Assert.Equal(8, sortedArray[1]);
            Assert.Equal(14, sortedArray[2]);
        }

        [Fact]
        public void InserMethodShouldInsertNewElementOnlyIfArrayRemainsSorted()
        {
            SortedIntArray sortedArray = new SortedIntArray();
            sortedArray.Add(5);
            sortedArray.Add(7);
            sortedArray.Add(9);
            sortedArray.Insert(2, 2);
            sortedArray.Insert(2, 8);
            sortedArray.Insert(0, 8);
            sortedArray.Insert(0, 3);
            Assert.Equal(3, sortedArray[0]);
            Assert.Equal(5, sortedArray[1]);
            Assert.Equal(7, sortedArray[2]);
            Assert.Equal(8, sortedArray[3]);
            Assert.Equal(9, sortedArray[4]);
        }

        [Fact]
        public void OtherMethodsShouldWorkTheSame()
        {
            SortedIntArray sortedArray = new SortedIntArray();
            sortedArray.Add(1);
            sortedArray.Add(129);
            sortedArray.Add(8);
            sortedArray.Add(90);
            sortedArray.Add(26);
            
            Assert.True(sortedArray.Contains(90));
            Assert.Equal(1, sortedArray.IndexOf(8));

            sortedArray.Remove(26);
            Assert.Equal(1, sortedArray[0]);
            Assert.Equal(8, sortedArray[1]);
            Assert.Equal(90, sortedArray[2]);
            Assert.Equal(129, sortedArray[3]);

            sortedArray.RemoveAt(3);
            Assert.Equal(3, sortedArray.Count);

            sortedArray.Clear();
            Assert.Equal(0, sortedArray.Count);
        }
    }
}
