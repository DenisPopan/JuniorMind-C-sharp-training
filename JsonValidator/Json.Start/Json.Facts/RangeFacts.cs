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
            Assert.True(range.Match(text1));
            Assert.True(range.Match(text2));
            Assert.True(range.Match(text3));
            Assert.False(range.Match(text4));
        }

        [Fact]
        public void MatchMethodShouldReturnFalseIfAStringIsNullOrEmpty()
        {
            var range = new Range('a', 'f');
            string text1 = null;
            string text2 = "";
            Assert.False(range.Match(text1));
            Assert.False(range.Match(text2));
        }
    }
}
