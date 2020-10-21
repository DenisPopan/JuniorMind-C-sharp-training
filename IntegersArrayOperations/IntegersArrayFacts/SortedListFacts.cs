using Xunit;
using IntegersArray;

namespace IntegersArrayFacts
{
    public class SortedListFacts
    {
        [Fact]
        public void AddMethodShouldSortListAfterEveryAddition()
        {
            SortedList<int> sortedList = new SortedList<int>();
            sortedList.Add(9);
            sortedList.Add(7);
            sortedList.Add(5);
            Assert.Equal(5, sortedList[0]);
            Assert.Equal(7, sortedList[1]);
            Assert.Equal(9, sortedList[2]);
            Assert.Equal(3, sortedList.Count);

            SortedList<string> sortedStringList = new SortedList<string>();
            sortedStringList.Add("house");
            sortedStringList.Add("dog");
            sortedStringList.Add("flowers");
            Assert.Equal("dog", sortedStringList[0]);
            Assert.Equal("flowers", sortedStringList[1]);
            Assert.Equal("house", sortedStringList[2]);
            Assert.Equal(3, sortedStringList.Count);

            var booleansSortedList = new SortedList<bool>();
            booleansSortedList.Add(true);
            booleansSortedList.Add(false);
            booleansSortedList.Add(true);

            Assert.True(booleansSortedList[1]);
            Assert.False(booleansSortedList[0]);
            Assert.True(booleansSortedList[2]);
        }

        [Fact]
        public void SetterShouldSetElementOnlyIfListRemainsSorted()
        {
            var sortedIntList = new SortedList<int>();
            sortedIntList.Add(16);
            sortedIntList.Add(467);
            sortedIntList.Add(162);
            sortedIntList.Add(356);
            sortedIntList.Add(1);
            sortedIntList[1] = 163;
            Assert.Equal(16, sortedIntList[1]);
            sortedIntList[3] = 355;
            Assert.Equal(355, sortedIntList[3]);
            sortedIntList[4] = 200;
            Assert.Equal(467, sortedIntList[4]);

            var sortedStringList = new SortedList<string>();
            sortedStringList.Add("house");
            sortedStringList.Add("dog");
            sortedStringList.Add("flowers");
            sortedStringList[0] = "bee";
            Assert.Equal("bee", sortedStringList[0]);
            Assert.Equal("flowers", sortedStringList[1]);
            Assert.Equal("house", sortedStringList[2]);
            sortedStringList[1] = "answer";
            Assert.Equal("flowers", sortedStringList[1]);
            sortedStringList[1] = "cat";
            Assert.Equal("cat", sortedStringList[1]);
        }

        [Fact]
        public void InserMethodShouldInsertNewElementOnlyIfListRemainsSorted()
        {
            var sortedIntList = new SortedList<int>();
            sortedIntList.Add(16);
            sortedIntList.Add(467);
            sortedIntList.Add(162);
            sortedIntList.Add(356);
            sortedIntList.Add(1);
            sortedIntList.Insert(1, 18);
            Assert.Equal(16, sortedIntList[1]);
            sortedIntList.Insert(1, 15);
            Assert.Equal(15, sortedIntList[1]);

            var sortedStringList = new SortedList<string>();
            sortedStringList.Add("house");
            sortedStringList.Add("dog");
            sortedStringList.Add("flowers");
            sortedStringList.Insert(0, "bee");
            Assert.Equal("bee", sortedStringList[0]);
            Assert.Equal("dog", sortedStringList[1]);
            Assert.Equal("flowers", sortedStringList[2]);
            Assert.Equal("house", sortedStringList[3]);
            sortedStringList.Insert(2, "fog");
            Assert.Equal("flowers", sortedStringList[2]);

            var sortedBoleansList = new SortedList<bool>();
            sortedBoleansList.Add(true);
            sortedBoleansList.Add(true);
            sortedBoleansList.Add(false);
            sortedBoleansList.Insert(0, true);
            Assert.False(sortedBoleansList[0]);
            Assert.True(sortedBoleansList[1]);
            Assert.True(sortedBoleansList[2]);
            sortedBoleansList.Insert(0, false);
            Assert.False(sortedBoleansList[0]);
            Assert.False(sortedBoleansList[1]);
            Assert.True(sortedBoleansList[2]);
            Assert.True(sortedBoleansList[3]);
        }

        [Fact]
        public void OtherMethodsShouldWorkTheSame()
        {
            var sortedArray = new SortedList<int>();
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
