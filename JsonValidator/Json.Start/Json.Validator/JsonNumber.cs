using System;

namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
        {
            return CanHaveMultipleDigits(input)
                && !ContainsLetters(input);
        }

        private static bool CanHaveMultipleDigits(string input)
        {
            return input.Length >= 1 && char.IsDigit(input[0]);
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
    }
}
