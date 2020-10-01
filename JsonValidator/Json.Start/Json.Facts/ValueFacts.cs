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

        [Fact]

        public void ValueCanBeNumber()
        {
            IMatch match1 = new Value().Match("654");
            IMatch match2 = new Value().Match("235.2353");
            IMatch match3 = new Value().Match("9847e23554");
            IMatch match4 = new Value().Match("234.345e66");
            IMatch match5 = new Value().Match("654.");
            IMatch match6 = new Value().Match("235ee");
            Assert.Equal((true, ""), (match1.Success(), match1.RemainingText()));
            Assert.Equal((true, ""), (match2.Success(), match2.RemainingText()));
            Assert.Equal((true, ""), (match3.Success(), match3.RemainingText()));
            Assert.Equal((true, ""), (match4.Success(), match4.RemainingText()));
            Assert.Equal((true, "."), (match5.Success(), match5.RemainingText()));
            Assert.Equal((true, "ee"), (match6.Success(), match6.RemainingText()));
        }
    }
}
