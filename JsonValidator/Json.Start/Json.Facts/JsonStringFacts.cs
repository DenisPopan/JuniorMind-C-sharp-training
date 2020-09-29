using Xunit;
using static Json.JsonString;

namespace Json.Facts
{
    public class JsonStringFacts
    {
        [Fact]
        public void IsWrappedInDoubleQuotes()
        {
            var string1 = new String().Match("abc");
            Assert.Equal((false, "abc"), (string1.Success(), string1.RemainingText()));
        }

        [Fact]
        public void AlwaysStartsWithQuotes()
        {
            var string1 = new String().Match("abc\"");
            Assert.Equal((false, "abc\""), (string1.Success(), string1.RemainingText()));
        }

        [Fact]
        public void AlwaysEndsWithQuotes()
        {
            var string1 = new String().Match("\"abc");
            Assert.Equal((false, "\"abc"), (string1.Success(), string1.RemainingText()));
        }

        [Fact]
        public void IsNotNull()
        {
            var string1 = new String().Match(null);
            Assert.Equal((false, null), (string1.Success(), string1.RemainingText()));
        }

        [Fact]
        public void IsNotAnEmptyString()
        {
            var string1 = new String().Match("");
            Assert.Equal((false, ""), (string1.Success(), string1.RemainingText()));
        }

        [Fact]
        public void IsAnEmptyDoubleQuotedString()
        {
            var string1 = new String().Match(Quoted(""));
            Assert.Equal((true, ""), (string1.Success(), string1.RemainingText()));
        }

        [Fact]
        public void HasStartAndEndQuotes()
        {
            var string1 = new String().Match("\"");
            Assert.Equal((false, "\""), (string1.Success(), string1.RemainingText()));
        }


        [Fact]
        public void DoesNotContainControlCharacters()
        {
            var string1 = new String().Match(Quoted("a\nb\rc"));
            Assert.Equal((false, "\"a\nb\rc\""), (string1.Success(), string1.RemainingText()));
        }

        [Fact]
        public void CanContainLargeUnicodeCharacters()
        {
            var string1 = new String().Match(Quoted("⛅⚾"));
            Assert.Equal((true, ""), (string1.Success(), string1.RemainingText()));

        }

        [Fact]
        public void CanContainEscapedQuotationMark()
        {
            var string1 = new String().Match(Quoted(@"\""a\"" b"));
            Assert.Equal((true, ""), (string1.Success(), string1.RemainingText()));
        }

        [Fact]
        public void CanContainEscapedReverseSolidus()
        {
            var string1 = new String().Match(Quoted(@"a \\ b"));
            Assert.Equal((true, ""), (string1.Success(), string1.RemainingText()));
        }

        [Fact]
        public void CanContainEscapedSolidus()
        {
            var string1 = new String().Match(Quoted(@"a \/ b"));
            Assert.Equal((true, ""), (string1.Success(), string1.RemainingText()));
        }

        [Fact]
        public void CanContainEscapedBackspace()
        {
            var string1 = new String().Match(Quoted(@"a \b b"));
            Assert.Equal((true, ""), (string1.Success(), string1.RemainingText()));
        }

        [Fact]
        public void CanContainEscapedFormFeed()
        {
            var string1 = new String().Match(Quoted(@"a \f b"));
            Assert.Equal((true, ""), (string1.Success(), string1.RemainingText()));
        }

        [Fact]
        public void CanContainEscapedLineFeed()
        {
            var string1 = new String().Match(Quoted(@"a \n b"));
            Assert.Equal((true, ""), (string1.Success(), string1.RemainingText()));

        }

        [Fact]
        public void CanContainEscapedCarrigeReturn()
        {
            var string1 = new String().Match(Quoted(@"a \r b"));
            Assert.Equal((true, ""), (string1.Success(), string1.RemainingText()));
        }

        [Fact]
        public void CanContainEscapedHorizontalTab()
        {
            var string1 = new String().Match(Quoted(@"a \t b"));
            Assert.Equal((true, ""), (string1.Success(), string1.RemainingText()));
        }

        [Fact]
        public void CanContainEscapedUnicodeCharacters()
        {
            var string1 = new String().Match(Quoted(@"a \u26Be b"));
            Assert.Equal((true, ""), (string1.Success(), string1.RemainingText()));
        }

        [Fact]
        public void DoesNotContainUnrecognizedExcapceCharacters()
        {
            var string1 = new String().Match(Quoted(@"a\x"));
            Assert.Equal((false, "\"a\\x\""), (string1.Success(), string1.RemainingText()));
        }

        [Fact]
        public void DoesNotEndWithReverseSolidus()
        {
            var string1 = new String().Match(Quoted(@"a\"));
            Assert.Equal((false, "\"a\\\""), (string1.Success(), string1.RemainingText()));
        }

        [Fact]
        public void DoesNotEndWithAnUnfinishedHexNumber()
        {
            var string1 = new String().Match(Quoted(@"a\u"));
            Assert.Equal((false, "\"a\\u\""), (string1.Success(), string1.RemainingText()));
            var string2 = new String().Match(Quoted(@"a\u123"));
            Assert.Equal((false, "\"a\\u123\""), (string2.Success(), string2.RemainingText()));
        }

        [Fact]
        public void IncorrectHexNumberShouldReturnFalse()
        {
            var string1 = new String().Match(Quoted(@"fos\uA46Q"));
            Assert.Equal((false, "\"fos\\uA46Q\""), (string1.Success(), string1.RemainingText()));
        }
        public static string Quoted(string text)
            => $"\"{text}\"";
    }
}
