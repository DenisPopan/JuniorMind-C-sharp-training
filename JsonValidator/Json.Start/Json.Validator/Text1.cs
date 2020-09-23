namespace Json
{
    public class Text1 : IPattern
    {
        readonly string prefix;

        public Text1(string prefix)
        {
            this.prefix = prefix;
        }

        public IMatch Match(string text)
        {
            return !string.IsNullOrEmpty(text) && text.StartsWith(prefix)
               ? new Match(text.Substring(prefix.Length), true)
               : new Match(text, false);
        }
    }
}
