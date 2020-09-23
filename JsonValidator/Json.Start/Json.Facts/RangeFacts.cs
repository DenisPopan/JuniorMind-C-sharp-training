using Xunit;

namespace Json.Facts
{
    public class RangeFacts
    {
        [Fact]
        public void MatchMethodShouldReturnIfTheFirstCharacterFromAStringIsInRange()
        {
            var range = new Range('a', 'f');
            IMatch match1 = range.Match("a");
            IMatch match2 = range.Match("fab");
            IMatch match3 = range.Match("bcd");
            IMatch match4 = range.Match("1ab");
            Assert.Equal((true, ""), (match1.Success(), match1.RemainingText()));
            Assert.Equal((true, "ab"), (match2.Success(), match2.RemainingText()));
            Assert.Equal((true, "cd"), (match3.Success(), match3.RemainingText()));
            Assert.Equal((false, "1ab"), (match4.Success(), match4.RemainingText()));
        }

        [Fact]
        public void MatchMethodShouldReturnFalseIfAStringIsNullOrEmpty()
        {
            var range = new Range('a', 'f');
            IMatch match1 = range.Match(null);
            IMatch match2 = range.Match("");
            Assert.Equal((false, null), (match1.Success(), match1.RemainingText()));
            Assert.Equal((false, ""), (match2.Success(), match2.RemainingText()));
        }
    }
}
