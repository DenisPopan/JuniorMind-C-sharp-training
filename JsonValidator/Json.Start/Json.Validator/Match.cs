namespace Json
{
    public class Match : IMatch
    {
        readonly string text;
        readonly bool condition;

        public Match(string text, bool condition)
        {
            this.text = text;
            this.condition = condition;
        }

        public bool Success()
        {
            return condition;
        }

        public string RemainingText()
        {
            if (text == null)
            {
                return null;
            }

            if (Success() && text.Length > 1)
            {
                return text.Substring(1);
            }

            return "";
        }
    }
}
