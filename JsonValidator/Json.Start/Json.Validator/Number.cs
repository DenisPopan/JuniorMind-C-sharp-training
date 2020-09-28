namespace Json
{
    public class Number : IPattern
    {
        private readonly IPattern pattern;

        public Number()
        {
            var digit = new Choice(new Character('0'), new Range('1', '9'));
            var onenine = new Range('1', '9');
            var digits = new Choice(new OneOrMore(digit), digit);
            var negative = new Optional(new Character('-'));

            var integer = new Choice(new Sequence(onenine, digits), digit);

            var fraction = new Optional(new Sequence(new Character('.'), digits));

            var exponentSymbol = new Any("eE");
            var sign = new Optional(new Any("-+"));

            var exponent = new Optional(new Sequence(exponentSymbol, sign, digits));

            pattern = new Sequence(negative, integer, fraction, exponent);
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
