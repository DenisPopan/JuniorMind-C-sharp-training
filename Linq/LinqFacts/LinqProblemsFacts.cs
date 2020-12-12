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
            var enumerator = new int[] {1, 2, 7, 2, 5, 7 }.SubarraysSum(14).GetEnumerator();
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
    }
}
