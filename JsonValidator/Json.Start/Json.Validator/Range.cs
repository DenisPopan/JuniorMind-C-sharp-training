namespace Json
{
    public class Range : IPattern
    {
        readonly char start;
        readonly char end;
        readonly string excepted;

        public Range(char start, char end)
        {
            this.start = start;
            this.end = end;
            excepted = "\\\"";
        }

        public IMatch Match(string text)
        {
            if (string.IsNullOrEmpty(text) || excepted.IndexOf(text[0]) != -1)
            {
                return new Match(text, false);
            }

            return text[0] >= start && text[0] <= end
                ? new Match(text.Substring(1), true)
                : new Match(text, false);
        }
    }
}
