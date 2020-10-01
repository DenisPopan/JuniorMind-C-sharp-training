using Xunit;

namespace Json.Facts
{
    public class ValueFacts
    {
        [Fact]

        public void ValueCanNotBeNull()
        {
            IMatch match = new Value().Match(null);
            Assert.Equal((false, null), (match.Success(), match.RemainingText()));
        }

        [Fact]

        public void ValueCanBeNullText()
        {
            IMatch match = new Value().Match("null");
            Assert.Equal((true, ""), (match.Success(), match.RemainingText()));
        }

        [Fact]

        public void ValueCanBeBoolean()
        {
            IMatch match1 = new Value().Match("true");
            IMatch match2 = new Value().Match("false");
            Assert.Equal((true, ""), (match1.Success(), match1.RemainingText()));
            Assert.Equal((true, ""), (match2.Success(), match2.RemainingText()));
        }
    }
}
