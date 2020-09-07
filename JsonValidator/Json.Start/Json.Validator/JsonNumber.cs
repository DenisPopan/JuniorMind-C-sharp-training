using System;

namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
        {
            return HasContent(input)
                && HasOnlyDigits(input);
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
