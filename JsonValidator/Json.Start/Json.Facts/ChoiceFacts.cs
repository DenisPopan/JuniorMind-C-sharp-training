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

        [Fact]
        public void ChoiceClassShouldReturnIfFirstCharIsPartOfAHexadecimalNumber()
        {
            var digit = new Choice(
            new Character('0'),
            new Range('1', '9')
            );

        var hex = new Choice(
        digit,
        new Choice(
            new Range('a', 'f'),
            new Range('A', 'F')
            )
        );

        hex.Match("012"); // true
            hex.Match("12"); // true
            hex.Match("92"); // true
            hex.Match("a9"); // true
            hex.Match("f8"); // true
            hex.Match("A9"); // true
            hex.Match("F8"); // true
            hex.Match("g8"); // false
            hex.Match("G8"); // false
            hex.Match(""); // false
            hex.Match(null); // false
        }

    }
}
