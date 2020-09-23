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
            IMatch match = new Match(text, true);
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
