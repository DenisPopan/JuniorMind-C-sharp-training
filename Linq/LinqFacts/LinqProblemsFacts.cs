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

        [Fact]
        public void MaxOccurenceMethodShouldReturnTheCharThatOccurrsTheMost()
        {
            Assert.Equal('a', "abcdsa".MaxOccurrence());
            Assert.Equal('h', "hey you hail".MaxOccurrence());
            Assert.Equal('a', "aeaoaaio".MaxOccurrence());
            Assert.Equal('-', "".MaxOccurrence());
        }

        [Fact]
        public void PalindromicPartitionsMethodShouldGenerateAllPalindromicPartitionsOfAString()
        {
            var enumerator = "aabaac".PalindromicPartitions().GetEnumerator();
            enumerator.MoveNext();
            Assert.Equal("a", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("aa", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("aabaa", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("a", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("aba", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("b", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("a", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("aa", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("a", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("c", enumerator.Current);
            Assert.False(enumerator.MoveNext());
        }

        [Fact]
        public void SubarraysSumMethodShouldReturnAllSubarraysWhoseSumIsLessOrEqualToK()
        {
            var enumerator = new int[] { 1, 2, 7, 2, 5, 7 }.SubarraysSum(14).GetEnumerator();
            enumerator.MoveNext();
            Assert.Equal(new int[] { 1 }, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(new int[] { 1, 2 }, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(new int[] { 1, 2, 7 }, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(new int[] { 1, 2, 7, 2 }, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(new int[] { 2 }, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(new int[] { 2, 7 }, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(new int[] { 2, 7, 2 }, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(new int[] { 7 }, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(new int[] { 7, 2 }, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(new int[] { 7, 2, 5 }, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(new int[] { 2 }, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(new int[] { 2, 5 }, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(new int[] { 2, 5, 7 }, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(new int[] { 5 }, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(new int[] { 5, 7 }, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(new int[] { 7 }, enumerator.Current);
            Assert.False(enumerator.MoveNext());
        }
        
        [Fact]

        public void SumCombinationsMethodShouldReturnAllCombinationsWhoseSumsIsEqualToK()
        {
            var enumerator = LinqProblems.SumCombinations(5, 1).GetEnumerator();
            enumerator.MoveNext();
            Assert.Equal("-1-2+3-4+5=1", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("+1-2+3+4-5=1", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("+1+2-3-4+5=1", enumerator.Current);
            Assert.False(enumerator.MoveNext());
        }

        [Fact]

        public void PythagoreanNumbersMethodShouldReturnAllPythagoreanNumbersCombinationsFromAGivenArray()
        {
            IEnumerable<int> array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var enumerator = array.PythagoreanNumbers().GetEnumerator();

            enumerator.MoveNext();
            Assert.Equal("[3, 4, 5]", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("[6, 8, 10]", enumerator.Current);
            Assert.False(enumerator.MoveNext());
        }

        [Fact]

        public void AtLeastOneFeatureMethodShouldReturnAllProductsThatHaveAtLeastOneFeatureFromAGivenList()
        {
            Feature f1 = new Feature { Id = 1 };
            Feature f2 = new Feature { Id = 2 };
            Feature f3 = new Feature { Id = 3 };
            Feature f4 = new Feature { Id = 4 };
            Feature f5 = new Feature { Id = 5 };
            Feature f6 = new Feature { Id = 6 };
            Feature f7 = new Feature { Id = 7 };
            var product1 = new Product { Name = "Phone", Features = new List<Feature> { f1, f3, f5, f7 } };
            var product2 = new Product { Name = "Tablet", Features = new List<Feature> { f2, f5, f6 } };
            var product3 = new Product { Name = "Laptop", Features = new List<Feature> { f3, f4, f5 } };
            var product4 = new Product { Name = "Mouse", Features = new List<Feature> { f1, f2, f5 } };
            var product5 = new Product { Name = "Keyboard", Features = new List<Feature> { f2, f4, f6, f7 } };
            var product6 = new Product { Name = "Camera", Features = new List<Feature> { f1, f3, f5 } };

            var productList = new List<Product> { product1, product2, product3, product4, product5, product6 };

            var featureList = new List<Feature> { f2, f4 };

            var enumerator = productList.AtLeastOneFeature(featureList).GetEnumerator();

            enumerator.MoveNext();
            Assert.Equal("Tablet", enumerator.Current.Name);
            enumerator.MoveNext();
            Assert.Equal("Laptop", enumerator.Current.Name);
            enumerator.MoveNext();
            Assert.Equal("Mouse", enumerator.Current.Name);
            enumerator.MoveNext();
            Assert.Equal("Keyboard", enumerator.Current.Name);
            Assert.False(enumerator.MoveNext());
        }

        [Fact]
        public void AllFeaturesMethodShouldReturnAllProductsThatHaveAllFeaturesFromAGivenList()
        {
            var f1 = new Feature { Id = 1 };
            var f2 = new Feature { Id = 2 };
            var f3 = new Feature { Id = 3 };
            var f4 = new Feature { Id = 4 };
            var f5 = new Feature { Id = 5 };
            var f6 = new Feature { Id = 6 };
            var f7 = new Feature { Id = 7 };
            var product1 = new Product { Name = "Phone", Features = new List<Feature> { f1, f3, f5, f7 } };
            var product2 = new Product { Name = "Tablet", Features = new List<Feature> { f2, f5, f6 } };
            var product3 = new Product { Name = "Laptop", Features = new List<Feature> { f3, f4, f5 } };
            var product4 = new Product { Name = "Mouse", Features = new List<Feature> { f1, f2, f5 } };
            var product5 = new Product { Name = "Keyboard", Features = new List<Feature> { f2, f4, f6, f7 } };
            var product6 = new Product { Name = "Camera", Features = new List<Feature> { f1, f3, f5 } };

            var productList = new List<Product> { product1, product2, product3, product4, product5, product6 };

            var featureList = new List<Feature> { f2, f4 };

            var enumerator = productList.AllFeatures(featureList).GetEnumerator();

            enumerator.MoveNext();
            Assert.Equal("Keyboard", enumerator.Current.Name);
            Assert.False(enumerator.MoveNext());
        }

        [Fact]
        public void NotTheseFeaturesMethodShouldReturnAllProductsThatHaveFeaturesBesidesThoseFromAGivenList()
        {
            var f1 = new Feature { Id = 1 };
            var f2 = new Feature { Id = 2 };
            var f3 = new Feature { Id = 3 };
            var f4 = new Feature { Id = 4 };
            var f5 = new Feature { Id = 5 };
            var f6 = new Feature { Id = 6 };
            var f7 = new Feature { Id = 7 };
            var product1 = new Product { Name = "Phone", Features = new List<Feature> { f1, f3, f5, f7 } };
            var product2 = new Product { Name = "Tablet", Features = new List<Feature> { f2, f5, f6 } };
            var product3 = new Product { Name = "Laptop", Features = new List<Feature> { f3, f4, f5 } };
            var product4 = new Product { Name = "Mouse", Features = new List<Feature> { f1, f2, f5 } };
            var product5 = new Product { Name = "Keyboard", Features = new List<Feature> { f2, f4, f6, f7 } };
            var product6 = new Product { Name = "Camera", Features = new List<Feature> { f1, f3, f5 } };

            var productList = new List<Product> { product1, product2, product3, product4, product5, product6 };

            var featureList = new List<Feature> { f2, f4, f7 };

            var enumerator = productList.NotTheseFeatures(featureList).GetEnumerator();

            enumerator.MoveNext();
            Assert.Equal("Camera", enumerator.Current.Name);
            Assert.False(enumerator.MoveNext());
        }

        [Fact]

        public void TotalQuantityMethodShouldReturnAListWithAllDistinctProductsAndTheTotalQuantity()
        {
            var product1 = new ProductStruct { Name = "banana", Quantity = 600 };
            var product2 = new ProductStruct { Name = "apple", Quantity = 200 };
            var product3 = new ProductStruct { Name = "grape", Quantity = 500 };
            var product4 = new ProductStruct { Name = "blueberry", Quantity = 400 };
            var product5 = new ProductStruct { Name = "apple", Quantity = 100 };
            var product6 = new ProductStruct { Name = "orange", Quantity = 200 };
            var product7 = new ProductStruct { Name = "banana", Quantity = 900 };
            var firstProductList = new ProductStruct[] { product1, product2, product3 };
            var secondProductList = new ProductStruct[] { product4, product5, product6, product7 };

            var enumerator = firstProductList.TotalQuantity(secondProductList).GetEnumerator();

            enumerator.MoveNext();
            Assert.Equal(("banana", 1500), (enumerator.Current.Name, enumerator.Current.Quantity));
            enumerator.MoveNext();
            Assert.Equal(("apple", 300), (enumerator.Current.Name, enumerator.Current.Quantity));
            enumerator.MoveNext();
            Assert.Equal(("grape", 500), (enumerator.Current.Name, enumerator.Current.Quantity));
            enumerator.MoveNext();
            Assert.Equal(("blueberry", 400), (enumerator.Current.Name, enumerator.Current.Quantity));
            enumerator.MoveNext();
            Assert.Equal(("orange", 200), (enumerator.Current.Name, enumerator.Current.Quantity));
            Assert.False(enumerator.MoveNext());
        }

        [Fact]

        public void HighestScoreMethodShouldReturnAListWithEachFamiliyHighestScore()
        {
            var test1 = new TestResults { Id = "1", FamilyId = "1", Score = 16 };
            var test2 = new TestResults { Id = "2", FamilyId = "2", Score = 4 };
            var test3 = new TestResults { Id = "3", FamilyId = "3", Score = 457 };
            var test4 = new TestResults { Id = "4", FamilyId = "4", Score = 253 };
            var test5 = new TestResults { Id = "5", FamilyId = "2", Score = 47 };
            var test6 = new TestResults { Id = "6", FamilyId = "3", Score = 1243 };
            var test7 = new TestResults { Id = "7", FamilyId = "1", Score = 12 };
            var test8 = new TestResults { Id = "8", FamilyId = "2", Score = 66 };

            var testResults = new TestResults[] { test1, test2, test3, test4, test5, test6, test7, test8 };

            var enumerator = testResults.HighestTestScore().GetEnumerator();
            enumerator.MoveNext();
            Assert.Equal(("1", 16), (enumerator.Current.FamilyId, enumerator.Current.Score));
            enumerator.MoveNext();
            Assert.Equal(("2", 66), (enumerator.Current.FamilyId, enumerator.Current.Score));
            enumerator.MoveNext();
            Assert.Equal(("3", 1243), (enumerator.Current.FamilyId, enumerator.Current.Score));
            enumerator.MoveNext();
            Assert.Equal(("4", 253), (enumerator.Current.FamilyId, enumerator.Current.Score));
            Assert.False(enumerator.MoveNext());
        }

        [Fact]
        public void TopWordsMethodShouldReturnTheTopThreeMostUsedWordsInAText()
        {
            string text = "hey Hey, how are you doing? I'm Daniel and this is Jhonny Silverhand, we're pleased to see you." +
                "we're also thrilled to work with You";
            var enumerator = text.TopWords().GetEnumerator();
            enumerator.MoveNext();
            Assert.Equal("you", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("hey", enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal("we're", enumerator.Current);
        }

        [Fact]

        public void IsSudokuValidMethodShouldValidateAGivenSudoku()
        {
            var sudoku = new int[9, 9] {
                { 9, 6, 8, 7, 1, 2, 5, 3, 4 },
                { 5, 3, 4, 6, 9, 8, 7, 2, 1 },
                { 1, 7, 2, 5, 4, 3, 9, 6, 8 },
                { 2, 9, 3, 4, 5, 6, 8, 1, 7 },
                { 7, 8, 1, 2, 3, 9, 4, 5, 6 },
                { 6, 4, 5, 8, 7, 1, 3, 9, 2 },
                { 4, 1, 7, 3, 6, 5, 2, 8, 9 },
                { 8, 5, 9, 1, 2, 4, 6, 7, 3 },
                { 3, 2, 6, 9, 8, 7, 1, 4, 5 }};

            Assert.True(sudoku.IsSudokuValid());

            var sudoku2 = new int[9, 9] {
                { 9, 6, 8, 7, 1, 2, 5, 3, 4 },
                { 5, 3, 4, 6, 9, 8, 7, 2, 1 },
                { 1, 7, 2, 5, 4, 3, 9, 6, 8 },
                { 2, 9, 2, 4, 5, 6, 8, 1, 7 },
                { 7, 8, 1, 2, 3, 9, 4, 5, 6 },
                { 6, 4, 5, 8, 7, 1, 3, 9, 2 },
                { 4, 1, 7, 3, 6, 5, 2, 8, 9 },
                { 8, 5, 9, 1, 2, 4, 6, 7, 3 },
                { 3, 2, 6, 9, 8, 7, 1, 4, 5 }};

            Assert.False(sudoku2.IsSudokuValid());
            var sudoku3 = new int[9, 9] {
                { 9, 6, 8, 7, 1, 2, 5, 3, 4 },
                { 5, 3, 4, 6, 9, 8, 7, 2, 1 },
                { 1, 7, 2, 5, 4, 3, 9, 6, 8 },
                { 2, 9, 3, 4, 5, 6, 8, 1, 7 },
                { 7, 8, 1, 2, 3, 9, 4, 5, 6 },
                { 6, 4, 5, 8, 7, 1, 3, 9, 2 },
                { 4, 1, 7, 3, 6, 5, 2, 8, 9 },
                { 8, 5, 9, 1, 2, 4, 6, 7, 3 },
                { 3, 2, 6, 0, 8, 7, 1, 4, 5 }};

            Assert.False(sudoku3.IsSudokuValid());
        }

        [Fact]

        public void PolishArithmeticResultMethodShouldReturnTheResultOfAPolishArithmeticOperation()
        {
            var operation1 = "234 346 55 568 + * -";
            var operation2 = "456 33 - 6762 *";
            var operation3 = "56 57 +";
            var operation4 = "12 4 /";
            var operation5 = "12 7 *";
            var operation6 = "345 4577 -";
            var operation7 = "234.7 567 /";
            var operation8 = "-3466796.235 567.3 778 9.354 - * /";

            Assert.Equal(-215324, operation1.PolishArithmeticResult());
            Assert.Equal(2860326, operation2.PolishArithmeticResult());
            Assert.Equal(113, operation3.PolishArithmeticResult());
            Assert.Equal(3, operation4.PolishArithmeticResult());
            Assert.Equal(84, operation5.PolishArithmeticResult());
            Assert.Equal(-4232, operation6.PolishArithmeticResult());
            Assert.Equal(0.4139329805996472, operation7.PolishArithmeticResult());
            Assert.Equal(-7.950403328127759, operation8.PolishArithmeticResult());
        }

        
    }
}
