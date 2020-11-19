using System;
using System.Collections.Generic;
using Linq;
using Xunit;

namespace LinqFacts
{
    public class LinqMethodsFacts
    {
        [Fact]
        public void AllMethodShouldReturnTrueIFAllElementsMeetTheGivenCondition()
        {
            var array = new int[3];
            array[0] = 6;
            array[1] = 8;
            array[2] = 10;

            var array1 = new int[2];
            array1[0] = 6;
            array1[1] = 9;

            Assert.True(LinqMethods.All<int>(array, c => c % 2 == 0));
            Assert.False(LinqMethods.All<int>(array1, c => c % 2 == 0));
            Assert.Throws<ArgumentNullException>(() => LinqMethods.All<int>(null, c => c % 2 == 0));
            Assert.Throws<ArgumentNullException>(() => LinqMethods.All<int>(array, null));
        }

        [Fact]
        public void AnyMethodShouldReturnTrueIFAnyElementMeetsTheGivenCondition()
        {
            var array = new int[3];
            array[0] = 6;
            array[1] = 7;
            array[2] = 10;

            var array1 = new int[2];
            array1[0] = 3;
            array1[1] = 9;

            Assert.True(LinqMethods.Any<int>(array, c => c % 2 == 0));
            Assert.False(LinqMethods.Any<int>(array1, c => c % 2 == 0));
            Assert.Throws<ArgumentNullException>(() => LinqMethods.Any<int>(null, c => c % 2 == 0));
            Assert.Throws<ArgumentNullException>(() => LinqMethods.Any<int>(array, null));
        }

        [Fact]
        public void FirstMethodShouldReturnFirstElementThatMeetsTheGivenCondition()
        {
            var array = new int[3];
            array[0] = 6;
            array[1] = 7;
            array[2] = 10;

            var array1 = new int[2];
            array1[0] = 3;
            array1[1] = 9;

            Assert.Equal(6, LinqMethods.First<int>(array, c => c % 2 == 0));
            Assert.Throws<InvalidOperationException>(() => LinqMethods.First<int>(array1, c => c % 2 == 0));
            Assert.Throws<ArgumentNullException>(() => LinqMethods.First<int>(null, c => c % 2 == 0));
            Assert.Throws<ArgumentNullException>(() => LinqMethods.First<int>(array, null));
        }

        [Fact]
        public void SelectMethodShouldReturnAModifiedGivenCollection()
        {
            var array = new int[3];
            array[0] = 6;
            array[1] = 7;
            array[2] = 10;

            var list = new List<string>();
            list = (List<string>)LinqMethods.Select<int, string>(array, c => c.ToString());

            Assert.Equal("6", list[0]);
            Assert.Equal("7", list[1]);
            Assert.Equal("10", list[2]);
        }

        [Fact]
        public void SelectManyMethodShouldReturnAModifiedGivenCollection()
        {
            var array = new int[6];
            array[0] = 6;
            array[1] = 7;
            array[2] = 10;

            var list = new List<string>();
            list = (List<string>)LinqMethods.SelectMany<int, string>(array, LinqMethods.SelectManySelector);

            Assert.Equal("6", list[0]);
            Assert.Equal("6", list[1]);
            Assert.Equal("7", list[2]);
            Assert.Equal("7", list[3]);
            Assert.Equal("10", list[4]);
            Assert.Equal("10", list[5]);
        }
    }
}
