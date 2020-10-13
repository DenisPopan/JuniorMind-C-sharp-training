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
            sortedArray.Add(9);
            sortedArray.Add(7);
            sortedArray.Add(5);
            Assert.Equal(5, sortedArray[0]);
            Assert.Equal(7, sortedArray[1]);
            Assert.Equal(9, sortedArray[2]);
        }

        [Fact]
        public void SetterShouldSortArrayAfterEverySet()
        {
            SortedIntArray sortedArray = new SortedIntArray();
            sortedArray.Add(9);
            sortedArray.Add(7);
            sortedArray.Add(5);
            sortedArray[0] = 10;
            Assert.Equal(7, sortedArray[0]);
            Assert.Equal(9, sortedArray[1]);
            Assert.Equal(10, sortedArray[2]);
            sortedArray[1] = 2;
            Assert.Equal(2, sortedArray[0]);
            Assert.Equal(7, sortedArray[1]);
            Assert.Equal(10, sortedArray[2]);
        }

        [Fact]
        public void InserMethodShouldSortArrayAfterEveryInsertion()
        {
            SortedIntArray sortedArray = new SortedIntArray();
            sortedArray.Add(9);
            sortedArray.Add(7);
            sortedArray.Add(5);
            sortedArray.Insert(2, 2);

            Assert.Equal(2, sortedArray[0]);
            Assert.Equal(5, sortedArray[1]);
            Assert.Equal(7, sortedArray[2]);
            Assert.Equal(9, sortedArray[3]);
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
