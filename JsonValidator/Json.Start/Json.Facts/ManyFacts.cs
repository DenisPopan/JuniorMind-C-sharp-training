using Xunit;

namespace Json.Facts
{
    public class ManyFacts
    {
        [Fact]
        public void ManyClassShouldApplyPatternAsLongAsItIsPosibleOnGivenText()
        {
            var a = new Many(new Character('a'));

            IMatch match1 = a.Match("abc");
            IMatch match2 = a.Match("aaaabc");
            IMatch match3 = a.Match("bc");
            IMatch match4 = a.Match("");
            IMatch match5 = a.Match(null);

            Assert.Equal((true, "bc"), (match1.Success(), match1.RemainingText()));
            Assert.Equal((true, "bc"), (match2.Success(), match2.RemainingText()));
            Assert.Equal((true, "bc"), (match3.Success(), match3.RemainingText()));
            Assert.Equal((true, ""), (match4.Success(), match4.RemainingText()));
            Assert.Equal((true, null), (match5.Success(), match5.RemainingText()));

            var digits = new Many(new Range('0', '9'));

            IMatch match6 = digits.Match("12345ab123");
            IMatch match7 = digits.Match("ab");

            Assert.Equal((true, "ab123"), (match6.Success(), match6.RemainingText()));
            Assert.Equal((true, "ab"), (match7.Success(), match7.RemainingText()));
        }
    }
}
