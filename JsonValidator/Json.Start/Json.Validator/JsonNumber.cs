using System;

namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
        {
            return HasContent(input)
                && HasOnlyDigits(input)
                && !StartsWithZero(input);
        }

        private static bool StartsWithZero(string input)
        {
            const byte minLength = 2;
            return input[0] == '0' && input.Length >= minLength;
        }

        private static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        private static bool HasOnlyDigits(string input)
        {
            foreach (char character in input)
            {
                if (!char.IsDigit(character))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
