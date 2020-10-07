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
            Assert.Equal(3, array.Count());
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
    }
}
