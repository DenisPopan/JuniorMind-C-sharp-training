using System;

namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
        {
            return HasContent(input)
                && ContentIsValid(input);
        }

        private static bool ContentIsValid(string input)
        {
            return NumberLengthIsAtLeastTwo(input) ? NumberStartIsValid(input)
               && HasOnlyAllowedCharacters(input) : char.IsDigit(input[0]);
        }

        private static bool NumberLengthIsAtLeastTwo(string input)
        {
            const byte numberMinLength = 2;
            return input.Length >= numberMinLength;
        }

        private static bool NumberStartIsValid(string input)
        {
            return (!IsZero(input)
                || IsNegative(input))
                && !IsNegativeAndStartsWithZero(input);
        }

        private static bool IsNegativeAndStartsWithZero(string input)
        {
            const byte minNumberLength = 3;
            if (input.Length < minNumberLength)
            {
                return false;
            }

            const byte thirdPosition = 2;
            return IsNegative(input) && input[1] == '0' && input[thirdPosition] == '0';
        }

        private static bool IsNegative(string input)
        {
            return input[0] == '-';
        }

        private static bool IsZero(string input)
        {
            return input[0] == '0';
        }

        private static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        private static bool HasOnlyAllowedCharacters(string input)
        {
            char[] allowedChars = { '.' };
            for (int i = 1; i < input.Length; i++)
            {
                if (!char.IsDigit(input[i]) && Array.IndexOf(allowedChars, input[i]) == -1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
