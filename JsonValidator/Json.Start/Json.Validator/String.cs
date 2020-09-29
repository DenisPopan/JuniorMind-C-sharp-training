namespace Json
{
    public class String : IPattern
    {
        private readonly IPattern pattern;

        public String()
        {
            var quotes = new Character('"');

            var digit = new Range('0', '9');
            var hex = new Choice(digit, new Range('a', 'f'), new Range('A', 'F'));
            var unicode = new Sequence(new Character('u'), hex, hex, hex, hex);
            var escapeKey = new Character('\\');
            var escape = new Choice(new Any("\"\\/bfnrt"), unicode);
            var unescapedChar = new Range(' ', char.MaxValue);
            var character = new Choice(unescapedChar, new Sequence(escapeKey, escape));
            var characters = new Many(character);

            pattern = new Sequence(quotes, characters, quotes);
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
