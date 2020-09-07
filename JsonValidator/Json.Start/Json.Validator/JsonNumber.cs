using System;

namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
        {
            return !IsNull(input)
                && CanHaveMultipleDigits(input);
        }

        private static bool IsNull(string input)
        {
            return input == null;
        }

        private static bool CanHaveMultipleDigits(string input)
        {
            foreach (char character in input)
            {
                if (char.IsDigit(character))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
