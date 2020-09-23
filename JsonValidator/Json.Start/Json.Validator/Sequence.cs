namespace Json
{
    public class Sequence : IPattern
    {
        readonly IPattern[] patterns;

        public Sequence(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            string textCopy = text;
            if (patterns.Length == 0)
            {
                return new Match("empty", true);
            }

            IMatch match = new Match("empty", false);
            foreach (var pattern in patterns)
            {
                match = pattern.Match(text);
                if (!match.Success())
                {
                    return new Match(textCopy, false);
                }

                text = match.RemainingText();
            }

            return match;
        }
    }
}
