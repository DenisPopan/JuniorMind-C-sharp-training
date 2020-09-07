using System;

namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
        {
            return IsZero(input)
                && !ContainsLetters(input);
        }

        private static bool ContainsLetters(string input)
        {
            foreach (char character in input)
            {
                if (char.IsLetter(character))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsZero(string input)
        {
            return input.IndexOf('0') != -1;
        }
    }
}
