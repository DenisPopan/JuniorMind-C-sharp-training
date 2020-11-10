using Xunit;
using IntegersArray;
using System.Collections.Generic;
using System;

namespace IntegersArrayFacts
{
    public class DictionaryFacts
    {
        [Fact]

        public void AddMethodShouldAddNewElement()
        {
            var dictionary = new IntegersArray.Dictionary<int, string>(10);
            dictionary.Add(6, "hey");
            Assert.Equal(1, dictionary.Count);
            Assert.Equal(dictionary[6], "hey");
            dictionary.Add(16, "oi!");
            Assert.Equal(2, dictionary.Count);
            Assert.Equal(dictionary[16], "oi!");
        }

        [Fact]

        public void AddMethodShouldThrowArgumentExceptionWhenElementWithGivenKeyAlreadyExists()
        {
            var dictionary = new IntegersArray.Dictionary<int, string>(10);
            dictionary.Add(6, "hey");
            Assert.Throws<ArgumentException>(() => dictionary.Add(6, "oi!"));
        }

        [Fact]

        public void AddMethodWithGivenKeyValuePairShouldAddNewElement()
        {
            var dictionary = new IntegersArray.Dictionary<int, string>(10);
            dictionary.Add(new KeyValuePair<int, string>(6, "hey"));
            Assert.Equal(1, dictionary.Count);
            Assert.Equal(dictionary[6], "hey");
            dictionary.Add((new KeyValuePair<int, string>(16, "oi")));
            Assert.Equal(2, dictionary.Count);
            Assert.Equal(dictionary[16], "oi");
        }

        [Fact]

        public void IndexerShouldGetTheElementValueWithGivenKey()
        {
            var dictionary = new IntegersArray.Dictionary<int, string>(10);
            dictionary.Add(new KeyValuePair<int, string>(6, "hey"));
            dictionary.Add((new KeyValuePair<int, string>(16, "oi")));
            dictionary.Add((new KeyValuePair<int, string>(8, "aii")));
            Assert.Equal(dictionary[6], "hey");
            Assert.Equal(dictionary[8], "aii");
        }

        [Fact]

        public void GetIndexerShouldThrowKeyNotFoundExceptionWhenKeyIsNotFound()
        {
            var dictionary = new IntegersArray.Dictionary<int, string>(10);
            dictionary.Add(new KeyValuePair<int, string>(6, "hey"));
            dictionary.Add((new KeyValuePair<int, string>(16, "oi")));
            dictionary.Add((new KeyValuePair<int, string>(8, "aii")));
            Assert.Throws<KeyNotFoundException>(() => dictionary[9]);
        }

        [Fact]

        public void SetIndexerShouldSetGivenKeyElementValueToAGivenOne()
        {
            var dictionary = new IntegersArray.Dictionary<int, string>(10);
            dictionary.Add(new KeyValuePair<int, string>(6, "hey"));
            dictionary.Add((new KeyValuePair<int, string>(16, "oi")));
            dictionary.Add((new KeyValuePair<int, string>(8, "aii")));
            
            Assert.Equal("aii", dictionary[8]);
            dictionary[8] = "haha";
            Assert.Equal("haha", dictionary[8]);

            dictionary[6] = "haha";
            Assert.Equal("haha", dictionary[6]);
        }

        [Fact]

        public void SetIndexerShouldThrowKeyNotFoundExceptionWhenKeyIsNotFound()
        {
            var dictionary = new IntegersArray.Dictionary<int, string>(10);
            dictionary.Add(new KeyValuePair<int, string>(6, "hey"));
            dictionary.Add((new KeyValuePair<int, string>(16, "oi")));
            dictionary.Add((new KeyValuePair<int, string>(8, "aii")));
            Assert.Throws<KeyNotFoundException>(() => dictionary[9] = "lala");
        }

        [Fact]

        public void ClearMethodShouldDeleteAllElements()
        {
            var dictionary = new IntegersArray.Dictionary<int, string>(10);
            dictionary.Add(6, "hey");
            dictionary.Add(16, "oi!");
            Assert.Equal(2, dictionary.Count);
            dictionary.Clear();
            Assert.Equal(0, dictionary.Count);
            Assert.Throws<KeyNotFoundException>(() => dictionary[6]);
            Assert.Throws<KeyNotFoundException>(() => dictionary[16]);
        }

        [Fact]

        public void ContainsMethodShouldReturnIfDictionaryContainsASpecificItem()
        {
            var dictionary = new IntegersArray.Dictionary<int, string>(10);
            dictionary.Add(new KeyValuePair<int, string>(6, "hey"));
            dictionary.Add((new KeyValuePair<int, string>(16, "oi")));
            dictionary.Add((new KeyValuePair<int, string>(8, "aii")));
            Assert.True(dictionary.Contains(new KeyValuePair<int, string>(6, "hey")));
            Assert.False(dictionary.Contains(new KeyValuePair<int, string>(6, "oi!")));
            Assert.Throws<KeyNotFoundException>(() => dictionary.Contains(new KeyValuePair<int, string>(7, "oi!")));
        }
    }
}
