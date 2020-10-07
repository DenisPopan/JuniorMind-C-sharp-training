using Xunit;
using IntegersArray;

namespace IntegersArrayFacts
{
    public class IntArrayFacts
    {
        [Fact]
        public void AddMethodsAddsNewElement()
        {
            IntArray array = new IntArray();
            array.Add(5);
            array.Add(222);
            array.Add(3263);
            Assert.Equal(3, array.Count());
        }
    }
}
