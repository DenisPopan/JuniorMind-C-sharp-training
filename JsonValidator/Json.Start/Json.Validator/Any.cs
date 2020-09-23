namespace Json
{
    public class Any : IPattern
    {
        readonly string accepted;

        public Any(string accepted)
        {
            this.accepted = accepted;
        }

        public IMatch Match(string text)
        {
            return !string.IsNullOrEmpty(text) && accepted.IndexOf(text[0]) != -1
                ? new Match(text.Substring(1), true)
                : new Match(text, false);
        }
    }
}