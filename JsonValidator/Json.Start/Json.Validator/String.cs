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
            var escape = new Choice(new Any("\"\\/bfnrt"), unicode);
            var unescapedChar = new Choice(new Range(' ', '!'), new Range('#', '['), new Range('^', (char)65535));
            var character = new Choice(unescapedChar, new Sequence(escapeKey, escape));
            var characters = new Many(character);

            pattern = new Sequence(startsWithDoubleQuotes, characters, endsWithDoubleQuotes);
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
