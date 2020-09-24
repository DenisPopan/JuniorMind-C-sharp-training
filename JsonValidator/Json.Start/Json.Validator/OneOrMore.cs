namespace Json
{
    public class OneOrMore
    {
        private readonly IPattern pattern;

        public OneOrMore(IPattern pattern)
        {
            this.pattern = new Sequence(new Choice(pattern), new Many(pattern));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
