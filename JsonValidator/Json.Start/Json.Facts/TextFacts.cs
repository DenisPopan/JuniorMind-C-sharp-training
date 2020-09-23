using Xunit;

namespace Json.Facts
{
    public class TextFacts
    {
        [Fact]
        public void TextClassShouldReturnIfAStringStartsWithAPrefix()
        {
            var True = new Text1("true");
            var False = new Text1("false");

            IMatch match1 = True.Match("true");
            IMatch match2 = True.Match("trueX");
            IMatch match3 = True.Match("false");
            IMatch match4 = True.Match("");
            IMatch match5 = True.Match(null);

            Assert.Equal((true, ""), (match1.Success(), match1.RemainingText()));
            Assert.Equal((true, "X"), (match2.Success(), match2.RemainingText()));
            Assert.Equal((false, "false"), (match3.Success(), match3.RemainingText()));
            Assert.Equal((false, ""), (match4.Success(), match4.RemainingText()));
            Assert.Equal((false, null), (match5.Success(), match5.RemainingText()));

            IMatch match6 = False.Match("false"); 
            IMatch match7 = False.Match("falseX");
            IMatch match8 = False.Match("true");
            IMatch match9 = False.Match("");
            IMatch match10 = False.Match(null);

            Assert.Equal((true, ""), (match6.Success(), match6.RemainingText()));
            Assert.Equal((true, "X"), (match7.Success(), match7.RemainingText()));
            Assert.Equal((false, "true"), (match8.Success(), match8.RemainingText()));
            Assert.Equal((false, ""), (match9.Success(), match9.RemainingText()));
            Assert.Equal((false, null), (match10.Success(), match10.RemainingText()));
        }
    }
}
