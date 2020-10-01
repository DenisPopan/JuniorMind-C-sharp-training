namespace Json
{
    public class Value : IPattern
    {
        private readonly IPattern pattern;

        public Value()
        {
            pattern = new Choice(new Text("null"));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
