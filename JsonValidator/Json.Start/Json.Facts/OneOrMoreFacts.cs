using Xunit;

namespace Json.Facts
{
    public class OneOrMoreFacts
    {
        [Fact]
        public void OneOrMoreClassShouldReturnIfPatternIsAppliedAtLeastOnce()
        {
            var a = new OneOrMore(new Range('0', '9'));
            IMatch match1 = a.Match("123");
            IMatch match2 = a.Match("1a"); 
            IMatch match3 = a.Match("bc");
            IMatch match4 = a.Match("");
            IMatch match5 = a.Match(null);

            Assert.Equal((true, ""), (match1.Success(), match1.RemainingText()));
            Assert.Equal((true, "a"), (match2.Success(), match2.RemainingText()));
            Assert.Equal((false, "bc"), (match3.Success(), match3.RemainingText()));
            Assert.Equal((false, ""), (match4.Success(), match4.RemainingText()));
            Assert.Equal((false, null), (match5.Success(), match5.RemainingText()));

        }
    }
}
