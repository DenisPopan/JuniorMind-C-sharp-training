namespace Json
{
    public class Value : IPattern
    {
        private readonly IPattern pattern;

        public Value()
        {
            var leftSquareBracket = new Character('[');
            var rightSquareBracket = new Character(']');

            var horizontalTab = new Character((char)9);
            var newLine = new Character((char)10);
            var carriageReturn = new Character((char)13);
            var space = new Character(' ');

            var whiteSpace = new Optional(new Choice(horizontalTab, newLine, carriageReturn, space));
            var element = new Sequence(whiteSpace, pattern, whiteSpace);
            var elements = new Choice(new List(element, new Character(',')), element);

            var array = new Choice(
                new Sequence(leftSquareBracket, whiteSpace, rightSquareBracket),
                new Sequence(leftSquareBracket, elements, rightSquareBracket));

            pattern = new Choice(
                array,
                new String(),
                new Number(),
                new Text("true"),
                new Text("false"),
                new Text("null"));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
