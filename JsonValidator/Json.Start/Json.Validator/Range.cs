using System;

namespace Json
{
    public class Range : IPattern
    {
        readonly char start;
        readonly char end;

        public Range(char start, char end)
        {
            this.start = start;
            this.end = end;
        }

        public IMatch Match(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new Match(text, false);
            }

            return new Match(text, text[0] >= start && text[0] <= end);
        }
    }
}
