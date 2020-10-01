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

        [Fact]
        public void ChoiceClassShouldBeAbleToAddNewPatternToItsPatterns()
        {
            var @string = new String();
            var number = new Number();
            var value = new Choice(
                @string,
                number,
                new Text("true"),
                new Text("false"),
                new Text("null")
            );

            var leftSquareBracket = new Character('[');
            var rightSquareBracket = new Character(']');

            var horizontalTab = new Character((char)9);
            var newLine = new Character((char)10);
            var carriageReturn = new Character((char)13);
            var space = new Character(' ');

            var whiteSpace = new Optional(new Choice(horizontalTab, newLine, carriageReturn, space));
            var element = new Sequence(whiteSpace, value, whiteSpace);
            var elements = new Choice(new List(element, new Character(',')), element);

            var array = new Choice(
                new Sequence(leftSquareBracket, whiteSpace, rightSquareBracket),
                new Sequence(leftSquareBracket, elements, rightSquareBracket));

            var leftCurlyBracket = new Character('{');
            var rightCurlyBracket = new Character('}');

            var member = new Sequence(whiteSpace, @string, whiteSpace, new Character(':'), element);
            var members = new Choice(new List(member, new Character(',')), member);

            var @object = new Choice(
                new Sequence(leftCurlyBracket, whiteSpace, rightCurlyBracket),
                new Sequence(leftCurlyBracket, members, rightCurlyBracket));


            value.Add(array);
            value.Add(@object);

            IMatch match1 = value.Match("\"hey\"");
            IMatch match2 = value.Match("2891e25");
            IMatch match3 = value.Match("true");
            IMatch match4 = value.Match("false");
            IMatch match5 = value.Match("null");
            IMatch match6 = value.Match("[ ]");
            IMatch match7 = value.Match("[ \"Ford\" ]");
            IMatch match8 = value.Match("[ \"Ford\", \"BMW\", \"Fiat\" ]");
            IMatch match9 = value.Match("{ }");
            IMatch match10 = value.Match("{ \"name\":\"John\" }");
            IMatch match11 = value.Match("{ \"name\":\"John\", \"age\":30 }");

            Assert.Equal((true, ""), (match1.Success(), match1.RemainingText()));
            Assert.Equal((true, ""), (match2.Success(), match2.RemainingText()));
            Assert.Equal((true, ""), (match3.Success(), match3.RemainingText()));
            Assert.Equal((true, ""), (match4.Success(), match4.RemainingText()));
            Assert.Equal((true, ""), (match5.Success(), match5.RemainingText()));
            Assert.Equal((true, ""), (match6.Success(), match6.RemainingText()));
            Assert.Equal((true, ""), (match7.Success(), match7.RemainingText()));
            Assert.Equal((true, ""), (match8.Success(), match8.RemainingText()));
            Assert.Equal((true, ""), (match9.Success(), match9.RemainingText()));
            Assert.Equal((true, ""), (match10.Success(), match8.RemainingText()));
            Assert.Equal((true, ""), (match11.Success(), match9.RemainingText()));

        }

    }
}
