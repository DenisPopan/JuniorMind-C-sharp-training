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
            IMatch match1 = digit.Match("012");
            IMatch match2 = digit.Match("12");
            IMatch match3 = digit.Match("92");
            IMatch match4 = digit.Match("a9");
            IMatch match5 = digit.Match("");
            IMatch match6 = digit.Match(null);
            Assert.Equal(("12", true), (match1.RemainingText(), match1.Success()));
            Assert.Equal(("2", true), (match2.RemainingText(), match2.Success())); 
            Assert.Equal(("2", true), (match3.RemainingText(), match3.Success()));
            Assert.Equal(("a9", false), (match4.RemainingText(), match4.Success()));
            Assert.Equal(("", false), (match5.RemainingText(), match5.Success()));
            Assert.Equal((null, false), (match6.RemainingText(), match6.Success())); 

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

            IMatch match1 = hex.Match("012");
            IMatch match2 = hex.Match("12");
            IMatch match3 = hex.Match("92");
            IMatch match4 = hex.Match("a9");
            IMatch match5 = hex.Match("f8");
            IMatch match6 = hex.Match("A9");
            IMatch match7 = hex.Match("F8");
            IMatch match8 = hex.Match("g8");
            IMatch match9 = hex.Match("G8");
            IMatch match10 = hex.Match("");
            IMatch match11 = hex.Match(null);
            Assert.Equal(("12", true), (match1.RemainingText(), match1.Success()));
            Assert.Equal(("2", true), (match2.RemainingText(), match2.Success()));
            Assert.Equal(("2", true), (match3.RemainingText(), match3.Success()));
            Assert.Equal(("9", true), (match4.RemainingText(), match4.Success()));
            Assert.Equal(("8", true), (match5.RemainingText(), match5.Success()));
            Assert.Equal(("9", true), (match6.RemainingText(), match6.Success()));
            Assert.Equal(("8", true), (match7.RemainingText(), match7.Success()));
            Assert.Equal(("g8", false), (match8.RemainingText(), match8.Success()));
            Assert.Equal(("G8", false), (match9.RemainingText(), match9.Success()));
            Assert.Equal(("", false), (match10.RemainingText(), match10.Success()));
            Assert.Equal((null, false), (match11.RemainingText(), match11.Success()));
        }

    }
}
