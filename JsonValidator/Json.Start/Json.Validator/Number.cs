namespace Json
{
    public class Number : IPattern
    {
        private readonly IPattern pattern;

        public Number()
        {
            var digit = new Choice(new Range('0', '9'));
            var onenine = new Range('1', '9');
            var digits = new OneOrMore(digit);
            var negative = new Optional(new Character('-'));

            var integer = new Sequence(negative, new Choice(new Sequence(onenine, digits), digit));

            var fraction = new Optional(new Sequence(new Character('.'), digits));

            var exponentSymbol = new Any("eE");
            var sign = new Optional(new Any("-+"));

            var exponent = new Optional(new Sequence(exponentSymbol, sign, digits));

            pattern = new Sequence(integer, fraction, exponent);
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
