using System;
using System.Collections.Generic;
using Linq;
using Xunit;

namespace LinqFacts
{
    public class LinqProblemsFacts
    {
        [Fact]
        public void VowelsNumberMethodShouldReturnVowelsNumberOfAString()
        {
            Assert.Equal(2, "abcdsa".VowelsNumber());
            Assert.Equal(0, "bcds".VowelsNumber());
            Assert.Equal(4, "aeio".VowelsNumber());
        }

        [Fact]
        public void ConsonantsNumberMethodShouldReturnConsonantsNumberOfAString()
        {
            Assert.Equal(4, "abcdsa".ConsonantsNumber());
            Assert.Equal(4, "bcds".ConsonantsNumber());
            Assert.Equal(0, "aeio".ConsonantsNumber());
        }

        [Fact]
        public void FirstUniqueMethodShouldReturnFirstUniqueCharWithinAString()
        {
            Assert.Equal('b', "abcdsa".FirstUnique());
            Assert.Equal('e', "hey you hail".FirstUnique());
            Assert.Equal('-', "aeioaeio".FirstUnique());
        }

        [Fact]
        public void ToIntMethodShouldConvertAStringToAnInteger()
        {
            Assert.Equal(123, "123".ToInt());
            Assert.Equal(-123, "-123".ToInt());
            Assert.Equal(23473434, "k2n3u4cb73db4y3/4t".ToInt());
        }
    }
}
