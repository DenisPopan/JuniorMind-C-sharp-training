using Xunit;

namespace Json.Facts
{
    public class ListFacts
    {
        [Fact]
        public void ListClassShouldReturnIFAListIsValid()
        {
            var a = new List(new Range('0', '9'), new Character(','));

            IMatch match1 = a.Match("1,2,3");
            IMatch match2 = a.Match("1,2,3,");
            IMatch match3 = a.Match("1a");
            IMatch match4 = a.Match("abc");
            IMatch match5 = a.Match("");
            IMatch match6 = a.Match(null);

            Assert.Equal((true, ""), (match1.Success(), match1.RemainingText()));
            Assert.Equal((true, ","), (match2.Success(), match2.RemainingText()));
            Assert.Equal((true, "a"), (match3.Success(), match3.RemainingText()));
            Assert.Equal((true, "abc"), (match4.Success(), match4.RemainingText()));
            Assert.Equal((true, ""), (match5.Success(), match5.RemainingText()));
            Assert.Equal((true, null), (match6.Success(), match6.RemainingText()));

            var digits = new OneOrMore(new Range('0', '9'));
            var whitespace = new Many(new Any(" \r\n\t"));
            var separator = new Sequence(whitespace, new Character(';'), whitespace);
            var list = new List(digits, separator);

            IMatch match7 = list.Match("1; 22  ;\n 333 \t; 22");
            IMatch match8 = list.Match("1 \n;");
            IMatch match9 = list.Match("abc");

            Assert.Equal((true, ""), (match7.Success(), match7.RemainingText()));
            Assert.Equal((true, " \n;"), (match8.Success(), match8.RemainingText()));
            Assert.Equal((true, "abc"), (match9.Success(), match9.RemainingText()));
        }
    }
}
