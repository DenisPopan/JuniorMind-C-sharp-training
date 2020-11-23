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

        [Fact]
        public void WhereMethodShouldReturnACollectionWithAllTheElementsThatMeetAGivenCondition()
        {
            var array = new int[6];
            array[0] = 6;
            array[1] = 7;
            array[2] = 10;
            array[3] = 15;
            array[4] = 18;
            array[5] = 22;

            var list = new List<int>();
            list = (List<int>)LinqMethods.Where<int>(array, c => c % 2 == 0);

            Assert.Equal(6, list[0]);
            Assert.Equal(10, list[1]);
            Assert.Equal(18, list[2]);
            Assert.Equal(22, list[3]);
        }

        [Fact]
        public void ToDictionaryMethodShouldReturnADictionaryWithAllTheElementsOfOurCollection()
        {
            var array = new int[6];
            array[0] = 6;
            array[1] = 7;
            array[2] = 10;
            array[3] = 15;
            array[4] = 18;
            array[5] = 22;

            var dictionary = new Dictionary<string, string>();
            dictionary = (Dictionary<string, string>)LinqMethods.ToDictionary<int, string, string>(array, c => c.ToString(), c => c.ToString());

            Assert.Equal("6", dictionary["6"]);
            Assert.Equal("10", dictionary["10"]);
            Assert.Equal("18", dictionary["18"]);
            Assert.Equal("15", dictionary["15"]);
            Assert.Equal("22", dictionary["22"]);
        }

        [Fact]
        public void ZipMethodShouldMergeTwoEnumerations()
        {
            var array = new int[4];
            array[0] = 6;
            array[1] = 7;
            array[2] = 10;
            array[3] = 15;

            var array1 = new int[5];
            array1[0] = 2;
            array1[1] = 3;
            array1[2] = 50;
            array1[3] = 7;

            var list = new List<int>();
            list = (List<int>)LinqMethods.Zip<int, int, int>(array, array1, (b, c) => b + c);

            Assert.Equal(8, list[0]);
            Assert.Equal(10, list[1]);
            Assert.Equal(60, list[2]);
            Assert.Equal(22, list[3]);

            array1[4] = 9;
            list.Clear();
            list = (List<int>)LinqMethods.Zip<int, int, int>(array, array1, (b, c) => b + c);

            Assert.Equal(8, list[0]);
            Assert.Equal(10, list[1]);
            Assert.Equal(60, list[2]);
            Assert.Equal(22, list[3]);
            Assert.Throws<ArgumentOutOfRangeException>(() => list[4]);
        }

        [Fact]
        public void AggregateMethodShouldApplyGivenFunctionOnAllSourceElements()
        {
            var array = new int[4];
            array[0] = 6;
            array[1] = 7;
            array[2] = 10;
            array[3] = 15;

            Assert.Equal(38, LinqMethods.Aggregate<int, int>(array, 0, (b, c) => b + c));
        }

        [Fact]
        public void JoinMethodShouldCorelateElementsOfTwoCollectionsBasedOnTheirKeys()
        {
            var array = new int[4];
            array[0] = 6;
            array[1] = 7;
            array[2] = 10;
            array[3] = 15;

            var array1 = new int[4];
            array1[0] = 2;
            array1[1] = 7;
            array1[2] = 50;
            array1[3] = 24;

            var list = new List<int>();
            list = (List<int>)LinqMethods.Join<int, int, string, int>(
                array, 
                array1, 
                a => a.ToString(),
                a => a.ToString(),
                (b, c) => b + c);

            Assert.Equal(14, list[0]);
            Assert.Throws<ArgumentOutOfRangeException>(() => list[1]);
        }

        [Fact]
        public void DistinctMethodShouldReturnAnEnumerationWithAllTheDistinctElements()
        {
            var array = new string[6];
            array[0] = "6";
            array[1] = "7";
            array[2] = "6";
            array[3] = "7";
            array[4] = "6";
            array[5] = "10";
            var list = new List<string>();
            list = (List<string>)LinqMethods.Distinct<string>(array, StringComparer.OrdinalIgnoreCase);

            Assert.Equal("6", list[0]);
            Assert.Equal("7", list[1]);
            Assert.Equal("10", list[2]);
            Assert.Throws<ArgumentOutOfRangeException>(() => list[3]);
        }

        [Fact]
        public void UnionMethodShouldReturnAnUnionOfTwoEnumerations()
        {
            var array = new int[8];
            array[0] = 5;
            array[1] = 3;
            array[2] = 9;
            array[3] = 7;
            array[4] = 5;
            array[5] = 9;
            array[6] = 3;
            array[7] = 7;

            var array1 = new int[8];
            array1[0] = 8;
            array1[1] = 3;
            array1[2] = 6;
            array1[3] = 4;
            array1[4] = 4;
            array1[5] = 9;
            array1[6] = 1;
            array1[7] = 0;

            var list = new List<int>();
            list = (List<int>)LinqMethods.Union<int>(
                array,
                array1,
                EqualityComparer<int>.Default);

            Assert.Equal(9, list.Count);
            Assert.Equal(5, list[0]);
            Assert.Equal(3, list[1]);
            Assert.Equal(9, list[2]);
            Assert.Equal(7, list[3]);
            Assert.Equal(8, list[4]);
            Assert.Equal(6, list[5]);
            Assert.Equal(4, list[6]);
            Assert.Equal(1, list[7]);
            Assert.Equal(0, list[8]);
            Assert.Throws<ArgumentOutOfRangeException>(() => list[9]);
        }
    }
}
