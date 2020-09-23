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
    }
}
