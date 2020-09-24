namespace Json
{
    public class Optional
    {
        readonly IPattern pattern;

        public Optional(IPattern pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(string text)
        {
            return new Match(pattern.Match(text).RemainingText(), true);
        }
    }
}
