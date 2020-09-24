using Xunit;

namespace Json.Facts
{
    public class OptionalFacts
    {
        [Fact]
        public void OptionalClassShouldApplyPatternOnlyOnTheFirstCharOfGivenText()
        {
            var a = new Optional(new Character('a'));
            IMatch match1 = a.Match("abc"); 
            IMatch match2 = a.Match("aabc");
            IMatch match3 = a.Match("bc");
            IMatch match4 = a.Match("");
            IMatch match5 = a.Match(null);

            Assert.Equal((true, "bc"), (match1.Success(), match1.RemainingText()));
            Assert.Equal((true, "abc"), (match2.Success(), match2.RemainingText()));
            Assert.Equal((true, "bc"), (match3.Success(), match3.RemainingText()));
            Assert.Equal((true, ""), (match4.Success(), match4.RemainingText()));
            Assert.Equal((true, null), (match5.Success(), match5.RemainingText()));
            // true / "bc"
            // true / "abc"
            // true / "bc"
            // true / ""
            // true / null

            var sign = new Optional(new Character('-'));

            IMatch match6 = sign.Match("123");
            IMatch match7 = sign.Match("-123");

            Assert.Equal((true, "123"), (match6.Success(), match6.RemainingText()));
            Assert.Equal((true, "123"), (match7.Success(), match7.RemainingText()));
        }
    }
}
