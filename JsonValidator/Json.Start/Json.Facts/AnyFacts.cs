using Xunit;

namespace Json.Facts
{
    public class AnyFacts
    {
        [Fact]
        public void AnyClassShouldReturnIfFirstCharOfAGivenStringIsPartOfAConstructorString()
        {
            var e = new Any("eE");
            IMatch match1 = e.Match("ea");
            IMatch match2 = e.Match("Ea");
            IMatch match3 = e.Match("a");
            IMatch match4 = e.Match("");
            IMatch match5 = e.Match(null);

            Assert.Equal((true, "a"), (match1.Success(), match1.RemainingText()));
            Assert.Equal((true, "a"), (match2.Success(), match2.RemainingText()));
            Assert.Equal((false, "a"), (match3.Success(), match3.RemainingText()));
            Assert.Equal((false, ""), (match4.Success(), match4.RemainingText()));
            Assert.Equal((false, null), (match5.Success(), match5.RemainingText()));
        }

        [Fact]
        public void AnyClassShouldReturnIfFirstCharOfAGivenTextIsASign()
        {
            var sign = new Any("-+");
            IMatch match1 = sign.Match("+3");
            IMatch match2 = sign.Match("-2");
            IMatch match3 = sign.Match("2");
            IMatch match4 = sign.Match("");
            IMatch match5 = sign.Match(null);

            Assert.Equal((true, "3"), (match1.Success(), match1.RemainingText()));
            Assert.Equal((true, "2"), (match2.Success(), match2.RemainingText()));
            Assert.Equal((false, "2"), (match3.Success(), match3.RemainingText()));
            Assert.Equal((false, ""), (match4.Success(), match4.RemainingText()));
            Assert.Equal((false, null), (match5.Success(), match5.RemainingText()));
        }
    }
}
