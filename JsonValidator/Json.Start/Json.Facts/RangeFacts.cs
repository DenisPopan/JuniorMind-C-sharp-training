using Xunit;

namespace Json.Facts
{
    public class RangeFacts
    {
        [Fact]
        public void MatchMethodShouldReturnIfTheFirstCharacterFromAStringIsInRange()
        {
            var range = new Range('a', 'f');
            string text1 = "abc";
            string text2 = "fab";
            string text3 = "bcd";
            string text4 = "1ab";
            Assert.Equal((true, "bc"), (range.Match(text1).Success(), range.Match(text1).RemainingText()));
            Assert.Equal((true, "ab"), (range.Match(text2).Success(), range.Match(text2).RemainingText()));
            Assert.Equal((true, "cd"), (range.Match(text3).Success(), range.Match(text3).RemainingText()));
            Assert.Equal((false, ""), (range.Match(text4).Success(), range.Match(text4).RemainingText()));
        }

        [Fact]
        public void MatchMethodShouldReturnFalseIfAStringIsNullOrEmpty()
        {
            var range = new Range('a', 'f');
            string text1 = null;
            string text2 = "";
            Assert.Equal((false, null), (range.Match(text1).Success(), range.Match(text1).RemainingText()));
            Assert.Equal((false, ""), (range.Match(text2).Success(), range.Match(text2).RemainingText()));
        }
    }
}
