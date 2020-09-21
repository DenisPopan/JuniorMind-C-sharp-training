using Xunit;

namespace Json.Facts
{
    public class ChoiceFacts
    {
        [Fact]
        public void ChoiceClassShouldReturnIfFirstCharIsADigit()
        {
            var digit = new Choice(
            new Character('0'),
            new Range('1', '9')
            );

            Assert.True(digit.Match("012")); // true
            Assert.True(digit.Match("12")); // true
            Assert.True(digit.Match("92")); // true
            Assert.False(digit.Match("a9")); // false
            Assert.False(digit.Match("")); // false
            Assert.False(digit.Match(null)); // false

        }
    }
}
