namespace Json
{
    public class String : IPattern
    {
        private readonly IPattern pattern;

        public String()
        {
            var startsWithDoubleQuotes = new Character('"');
            var endsWithDoubleQuotes = new Character('"');

            var digit = new Range('0', '9');
            var hex = new Choice(digit, new Range('a', 'f'), new Range('A', 'F'));
            var unicode = new Sequence(new Character('u'), hex, hex, hex, hex);
            var escapeKey = new Character('\\');
            var escape = new Sequence(escapeKey, new Choice(new Any("\"\\/bfnrt"), unicode));

            var character = new Choice(new Character(' '), new Range('a', 'z'), new Range('A', 'Z'), escape);
            var characters = new Optional(new OneOrMore(character));

            pattern = new Sequence(startsWithDoubleQuotes, characters, endsWithDoubleQuotes);
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
