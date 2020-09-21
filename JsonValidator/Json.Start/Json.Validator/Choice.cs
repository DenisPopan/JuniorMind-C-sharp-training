namespace Json
{
    public class Choice
    {
        readonly IPattern[] patterns;

        public Choice(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public bool Match(string text)
        {
            int numberOfTrueCases = 0;
            foreach (var pattern in patterns)
            {
                if (pattern.Match(text))
                {
                    numberOfTrueCases++;
                }
            }

            return numberOfTrueCases > 0;
        }
    }
}
