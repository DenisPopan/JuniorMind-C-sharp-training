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

            var whiteSpace = new Many(new Any("\t\n\r "));
            var element = new Sequence(whiteSpace, value, whiteSpace);
            var elements = new List(element, new Character(','));

            var array = new Sequence(leftSquareBracket, new Choice(elements, whiteSpace), rightSquareBracket);

            var leftCurlyBracket = new Character('{');
            var rightCurlyBracket = new Character('}');

            var member = new Sequence(whiteSpace, new String(), whiteSpace, new Character(':'), element);
            var members = new List(member, new Character(','));

            var @object = new Sequence(leftCurlyBracket, new Choice(members, whiteSpace), rightCurlyBracket);

            value.Add(array);
            value.Add(@object);
            pattern = element;
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
