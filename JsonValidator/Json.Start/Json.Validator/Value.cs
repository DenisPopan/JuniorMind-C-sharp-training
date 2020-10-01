namespace Json
{
    public class Value : IPattern
    {
        private readonly IPattern pattern;

        public Value()
        {
            var value = new Choice(
                new String(),
                new Number(),
                new Text("true"),
                new Text("false"),
                new Text("null"));

            var leftSquareBracket = new Character('[');
            var rightSquareBracket = new Character(']');

            var whiteSpace = new Optional(new Any("\t\n\r "));
            var element = new Sequence(whiteSpace, value, whiteSpace);
            var elements = new List(element, new Character(','));

            var array = new Sequence(leftSquareBracket, new Choice(whiteSpace, elements), rightSquareBracket);
            value.Add(array);
            pattern = value;
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
