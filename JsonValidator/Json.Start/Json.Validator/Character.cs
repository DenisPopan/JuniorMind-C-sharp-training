namespace Json
{
    public class Character : IPattern
    {
        readonly char pattern;

        public Character(char pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(string text)
        {
            return !string.IsNullOrEmpty(text) && text[0] == pattern
                ? new Match(text.Substring(1), true)
                : new Match(text, false);
        }
    }
}
