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

            Assert.Equal(("12", true), (digit.Match("012").RemainingText(), digit.Match("012").Success()));
            Assert.Equal(("2", true), (digit.Match("12").RemainingText(), digit.Match("12").Success())); 
            Assert.Equal(("2", true), (digit.Match("92").RemainingText(), digit.Match("92").Success()));
            Assert.Equal(("", false), (digit.Match("a9").RemainingText(), digit.Match("a9").Success()));
            Assert.Equal(("", false), (digit.Match("").RemainingText(), digit.Match("").Success()));
            Assert.Equal((null, false), (digit.Match(null).RemainingText(), digit.Match(null).Success())); 

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

            Assert.Equal(("12", true), (hex.Match("012").RemainingText(), hex.Match("012").Success()));
            Assert.Equal(("2", true), (hex.Match("12").RemainingText(), hex.Match("12").Success()));
            Assert.Equal(("2", true), (hex.Match("92").RemainingText(), hex.Match("92").Success()));
            Assert.Equal(("9", true), (hex.Match("a9").RemainingText(), hex.Match("a9").Success()));
            Assert.Equal(("8", true), (hex.Match("f8").RemainingText(), hex.Match("f8").Success()));
            Assert.Equal(("9", true), (hex.Match("A9").RemainingText(), hex.Match("A9").Success()));
            Assert.Equal(("8", true), (hex.Match("F8").RemainingText(), hex.Match("F8").Success()));
            Assert.Equal(("", false), (hex.Match("g8").RemainingText(), hex.Match("g8").Success()));
            Assert.Equal(("", false), (hex.Match("G8").RemainingText(), hex.Match("G8").Success()));
            Assert.Equal(("", false), (hex.Match("").RemainingText(), hex.Match("").Success()));
            Assert.Equal((null, false), (hex.Match(null).RemainingText(), hex.Match(null).Success()));
        }

    }
}
