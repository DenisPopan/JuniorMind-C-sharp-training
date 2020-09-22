namespace Json
{
    public class Match : IMatch
    {
        readonly string text;
        readonly bool condition;

        public Match(string text, bool condition)
        {
            if (text == null)
            {
                this.text = null;
            }
            else if (condition)
            {
                this.text = text.Length > 1 ? text.Substring(1) : "";
            }
            else
            {
                this.text = text;
            }

            this.condition = condition;
        }

        public bool Success()
        {
            return condition;
        }

        public string RemainingText()
        {
            return text;
        }
    }
}
