using Xunit;
using IntegersArray;
using System;

namespace IntegersArrayFacts
{
    public class DictionaryFacts
    {
        [Fact]

        public void AddMethodShouldAddNewElement()
        {
            var dictionary = new Dictionary<int, string>(10);
            dictionary.Add(6, "hey");
            Assert.Equal(1, dictionary.Count);
            dictionary.Add(6, "oi!");
            Assert.Equal(2, dictionary.Count);
        }
    }
}
