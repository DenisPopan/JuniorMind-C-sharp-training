using Xunit;
using IntegersArray;

namespace IntegersArrayFacts
{
    public class IntArrayFacts
    {
        [Fact]
        public void AddMethodAddsNewElement()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(222);
            array.Add(3263);
            array.Add(357);
            Assert.Equal(4, array.Count());
            array.Add(783);
            Assert.Equal(5, array.Count());
            Assert.Equal(783, array.Element(4));
        }

        [Fact]
        public void ElementMethodReturnsCorrectElement()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(222);
            array.Add(3263);
            Assert.Equal(5, array.Element(0));
            Assert.Equal(222, array.Element(1));
            Assert.Equal(3263, array.Element(2));
        }

        [Fact]
        public void ElementMethodShouldReturnMinusOneWhenElementIsOutsideOfArray()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(222);
            array.Add(3263);
            Assert.Equal(-1, array.Element(-1));
            Assert.Equal(-1, array.Element(4));
        }

        [Fact]
        public void SetElementMethodShouldSetElementAtGivenIndexWithGivenValue()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(222);
            array.Add(3263);
            Assert.Equal(3263, array.Element(2));
            array.SetElement(2, 716);
            Assert.Equal(716, array.Element(2));
        }

        [Fact]
        public void SetElementMethodShouldLeaveElementUnchangedIfGivenIndexIsInvalid()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(222);
            array.Add(3263);
            array.SetElement(-1, 716);
            array.SetElement(4, 716);
            Assert.Equal(5, array.Element(0));
            Assert.Equal(222, array.Element(1));
            Assert.Equal(3263, array.Element(2));
        }

        [Fact]
        public void ContainsMethodShouldReturnIfArrayContainsGivenElement()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(222);
            array.Add(3263);
            Assert.True(array.Contains(5));
            Assert.False(array.Contains(223));
        }

        [Fact]
        public void IndexOfMethodShouldReturnGivenElementIndex()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(222);
            array.Add(3263);
            Assert.Equal(0, array.IndexOf(5));
            Assert.Equal(2, array.IndexOf(3263));
            Assert.Equal(-1, array.IndexOf(223));
        }

        [Fact]
        public void InsertMethodShouldInsertGivenElementOnGivenRandomIndex()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(222);
            array.Add(3263);
            array.Insert(1, 71);
            Assert.Equal(4, array.Count());
            Assert.Equal(5, array.Element(0));
            Assert.Equal(71, array.Element(1));
            Assert.Equal(222, array.Element(2));
            Assert.Equal(3263, array.Element(3));
        }

        [Fact]
        public void InsertMethodShouldInsertGivenElementOnFirstAndLastIndexAlso()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(222);
            array.Add(3263);
            array.Insert(0, 71);
            array.Insert(3, 93471);
            Assert.Equal(5, array.Count());
            Assert.Equal(71, array.Element(0));
            Assert.Equal(5, array.Element(1));
            Assert.Equal(222, array.Element(2));
            Assert.Equal(93471, array.Element(3));
            Assert.Equal(3263, array.Element(4));
        }

        [Fact]
        public void InsertMethodShouldMakeNoChangesIfGivenIndexIsOutsideOfArray()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(222);
            array.Add(3263);
            array.Insert(0, 71);
            array.Insert(4, 93471);
            array.Insert(5, 811);
            Assert.Equal(71, array.Element(0));
            Assert.Equal(5, array.Element(1));
            Assert.Equal(222, array.Element(2));
            Assert.Equal(3263, array.Element(3));
            Assert.Equal(-1, array.Element(4));
            Assert.Equal(-1, array.Element(5));
        }

        [Fact]
        public void ClearMethodShouldDeleteAllArrayElements()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(222);
            array.Add(3263);
            array.Clear();
            Assert.Equal(0, array.Element(0));
            Assert.Equal(0, array.Element(1));
            Assert.Equal(0, array.Element(2));
        }

        [Fact]
        public void RemoveMethodShouldDeleteFirstOccurenceOfGivenElement()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(222);
            array.Add(3263);
            array.Add(2246);
            array.Remove(222);
            Assert.Equal(5, array.Element(0));
            Assert.Equal(3263, array.Element(1));
            Assert.Equal(2246, array.Element(2));
            Assert.Equal(0, array.Element(3));
            array.Remove(2246);
            Assert.Equal(0, array.Element(2));
        }

        [Fact]
        public void RemoveMethodShouldMakeNoChangesIfElementDoesNotExist()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(222);
            array.Add(3263);
            array.Add(2246);
            array.Remove(223);
            Assert.Equal(5, array.Element(0));
            Assert.Equal(222, array.Element(1));
            Assert.Equal(3263, array.Element(2));
            Assert.Equal(2246, array.Element(3));
        }

        [Fact]
        public void RemoveAtMethodShouldDeleteElementAtGivenIndex()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(222);
            array.Add(3263);
            array.Add(2246);
            array.RemoveAt(1);
            Assert.Equal(5, array.Element(0));
            Assert.Equal(3263, array.Element(1));
            Assert.Equal(2246, array.Element(2));
            Assert.Equal(0, array.Element(3));
            array.RemoveAt(2);
            Assert.Equal(0, array.Element(2));
        }

        [Fact]
        public void RemoveAtMethodShouldMakeNoChangesIfGivenIndexIsOutsideOfArray()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(222);
            array.Add(3263);
            array.Add(2246);
            array.RemoveAt(-1);
            array.RemoveAt(4);
            Assert.Equal(5, array.Element(0));
            Assert.Equal(222, array.Element(1));
            Assert.Equal(3263, array.Element(2));
            Assert.Equal(2246, array.Element(3));
        }
    }
}




