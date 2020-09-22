using System;

namespace Json
{
    public class Character : IPattern
    {
        readonly char pattern;

        public Character(char pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new Match(text, false);
            }

            return new Match(text, text[0] == pattern);
        }
    }
}
