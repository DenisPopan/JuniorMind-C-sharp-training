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
            var enumerator = LinqMethods.Select<int, string>(new[] {6, 7, 10 }, c => c.ToString()).GetEnumerator();
            enumerator.MoveNext();
            Assert.Equal("6", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("7", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("10", enumerator.Current);
            Assert.False(enumerator.MoveNext());
        }

        [Fact]
        public void SelectManyMethodShouldReturnAModifiedGivenCollection()
        {
            var array = new int[3];
            array[0] = 6;
            array[1] = 7;
            array[2] = 10;

            var enumerator = LinqMethods.SelectMany<int, string>(array, LinqMethods.SelectManySelector).GetEnumerator();

            enumerator.MoveNext();
            Assert.Equal("6", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("6", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("7", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("7", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("10", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("10", enumerator.Current);
            Assert.False(enumerator.MoveNext());
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

            var enumerator = LinqMethods.Where<int>(array, c => c % 2 == 0).GetEnumerator();
            enumerator.MoveNext();
            Assert.Equal(6, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(10, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(18, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(22, enumerator.Current);
            Assert.False(enumerator.MoveNext());

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

            var enumerator = LinqMethods.Zip<int, int, int>(array, array1, (b, c) => b + c).GetEnumerator();

            enumerator.MoveNext();
            Assert.Equal(8, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(10, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(60, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(22, enumerator.Current);
            Assert.False(enumerator.MoveNext());

            array1[4] = 9;
            var enumerator1 = LinqMethods.Zip<int, int, int>(array, array1, (b, c) => b + c).GetEnumerator();

            enumerator1.MoveNext();
            Assert.Equal(8, enumerator1.Current);
            enumerator1.MoveNext();
            Assert.Equal(10, enumerator1.Current);
            enumerator1.MoveNext();
            Assert.Equal(60, enumerator1.Current);
            enumerator1.MoveNext();
            Assert.Equal(22, enumerator1.Current);
            Assert.False(enumerator1.MoveNext());
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

            var enumerator = LinqMethods.Join<int, int, string, int>(
                array, 
                array1, 
                a => a.ToString(),
                a => a.ToString(),
                (b, c) => b + c).GetEnumerator();

            enumerator.MoveNext();

            Assert.Equal(14, enumerator.Current);
            Assert.False(enumerator.MoveNext());
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

            var enumerator = LinqMethods.Distinct<string>(array, StringComparer.OrdinalIgnoreCase).GetEnumerator();

            enumerator.MoveNext();
            Assert.Equal("6", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("7", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("10", enumerator.Current);
            Assert.False(enumerator.MoveNext());
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

            var enumerator = LinqMethods.Union<int>(
                array,
                array1,
                EqualityComparer<int>.Default).GetEnumerator();

            enumerator.MoveNext();
            Assert.Equal(5, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(3, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(9, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(7, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(8, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(6, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(4, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(1, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(0, enumerator.Current);
            Assert.False(enumerator.MoveNext());
        }

        [Fact]
        public void IntersectMethodShouldReturnAnIntersectionOfTwoGivenEnumerations()
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

            var enumerator = LinqMethods.Intersect<int>(
                array,
                array1,
                EqualityComparer<int>.Default).GetEnumerator();

            enumerator.MoveNext();
            Assert.Equal(3, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(9, enumerator.Current);
            Assert.False(enumerator.MoveNext());
        }

        [Fact]
        public void ExceptMethodShouldReturnAnEnumerationsOfAllElementsFromFirstEnumerationThatDoNotAppearInTheSecond()
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

            var enumerator = LinqMethods.Except<int>(
                array,
                array1,
                EqualityComparer<int>.Default).GetEnumerator();

            enumerator.MoveNext();
            Assert.Equal(5, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(7, enumerator.Current);
            Assert.False(enumerator.MoveNext());
        }

        [Fact]
        public void GroupByMethodShouldGroupElementsByTheirKeysAndReturnAnEnumerationOfPropertiesOfTheseGroups()
        {
            var enumerator = LinqMethods.GroupBy<int, int, int, string>(
                new[] { 6,7,8,9,6,6,7,8,7 },
                a => a.GetHashCode(),
                a => a,
                LinqMethods.Count,
                EqualityComparer<int>.Default).GetEnumerator();

            enumerator.MoveNext();
            Assert.Equal("6:3", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("7:3", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("8:2", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("9:1", enumerator.Current);
            Assert.False(enumerator.MoveNext());
        }

        [Fact]
        public void OderByMethodShouldOrderElementsByTheirKey()
        {
            var enumerator = LinqMethods.OrderBy<int, int>(
                new[] { 6, 7, 8, 9, 26, 16, 17, 18, 27 },
                a => Math.Abs(a.GetHashCode() % 10),
                Comparer<int>.Default).GetEnumerator();

            enumerator.MoveNext();
            Assert.Equal(6, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(26, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(16, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(7, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(17, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(27, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(8, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(18, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(9, enumerator.Current);
            Assert.False(enumerator.MoveNext());
            
            // checked if it works for descending order
            // and it does

            /*enumerator.MoveNext();
            Assert.Equal(9, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(8, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(18, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(7, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(17, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(27, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(6, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(26, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(16, enumerator.Current);
            Assert.False(enumerator.MoveNext());*/
        }
    }
}
