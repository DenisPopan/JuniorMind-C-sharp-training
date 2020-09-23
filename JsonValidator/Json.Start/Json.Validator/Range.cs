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

            if (text[0] >= start && text[0] <= end)
            {
                text = text.Remove(0, 1);
                return new Match(text, true);
            }

            return new Match(text, false);
        }
    }
}
