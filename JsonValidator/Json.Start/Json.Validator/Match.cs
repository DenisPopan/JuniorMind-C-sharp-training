namespace Json
{
    internal class Match : IMatch
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
            return text;
        }
    }
}
