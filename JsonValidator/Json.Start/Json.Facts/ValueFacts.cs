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
    }
}
