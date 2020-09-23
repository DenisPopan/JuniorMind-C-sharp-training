using Xunit;

namespace Json.Facts
{
    public class SequenceFacts
    {
        [Fact]
        public void SequenceClassMatchMethodShouldReturnIfASequenceIsValid()
        {
            var ab = new Sequence(
                new Character('a'),
                new Character('b')
                );

            IMatch match1 = ab.Match("abcd");
            IMatch match2 = ab.Match("ax");
            IMatch match3 = ab.Match("def");
            IMatch match4 = ab.Match("");
            IMatch match5 = ab.Match(null);

            Assert.Equal((true, "cd"), (match1.Success(), match1.RemainingText()));
            Assert.Equal((false, "ax"), (match2.Success(), match2.RemainingText()));
            Assert.Equal((false, "def"), (match3.Success(), match3.RemainingText()));
            Assert.Equal((false, ""), (match4.Success(), match4.RemainingText()));
            Assert.Equal((false, null), (match5.Success(), match5.RemainingText()));

            var abc = new Sequence(
                ab,
                new Character('c')
            );

            IMatch match6 = abc.Match("abcd");
            IMatch match7 = abc.Match("ax");
            IMatch match8 = abc.Match("def");
            IMatch match9 = abc.Match("");
            IMatch match10 = abc.Match(null);

            Assert.Equal((true, "d"), (match6.Success(), match6.RemainingText()));
            Assert.Equal((false, "ax"), (match7.Success(), match7.RemainingText()));
            Assert.Equal((false, "def"), (match8.Success(), match8.RemainingText()));
            Assert.Equal((false, ""), (match9.Success(), match9.RemainingText()));
            Assert.Equal((false, null), (match10.Success(), match10.RemainingText()));
        }

        [Fact]
        public void SequenceClassMatchMethodShouldReturnIfASequenceIsAHexNumber()
        {
            var hex = new Choice(
            new Range('0', '9'),
            new Range('a', 'f'),
            new Range('A', 'F')
            );

            var hexSeq = new Sequence(
                new Character('u'),
                new Sequence(
                    hex,
                    hex,
                    hex,
                    hex
                )
            );

            IMatch match1 = hexSeq.Match("u1234");
            IMatch match2 = hexSeq.Match("uabcdef");
            IMatch match3 = hexSeq.Match("uB005 ab");
            IMatch match4 = hexSeq.Match("abc");
            IMatch match5 = hexSeq.Match(null);
            Assert.Equal((true, ""), (match1.Success(), match1.RemainingText()));
            Assert.Equal((true, "ef"), (match2.Success(), match2.RemainingText()));
            Assert.Equal((true, " ab"), (match3.Success(), match3.RemainingText()));
            Assert.Equal((false, "abc"), (match4.Success(), match4.RemainingText()));
            Assert.Equal((false, null), (match5.Success(), match5.RemainingText()));
        }
    }
}
