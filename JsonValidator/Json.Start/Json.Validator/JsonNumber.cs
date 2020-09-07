using System;

namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
        {
            return input.IndexOf('0') != -1
                && !ContainsLetters(input);
        }

        private static bool ContainsLetters(string input)
        {
            return false;
        }
    }
}
