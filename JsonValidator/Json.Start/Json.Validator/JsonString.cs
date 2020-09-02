using System;

namespace Json
{
    public static class JsonString
    {
        public static bool IsJsonString(string input)
        {
            return HasContent(input)
                && IsDoubleQuoted(input)
                && !ContainsControlCharacters(input);
        }

        private static bool ContainsControlCharacters(string input)
        {
            const byte maxAsciiValue = 32;
            foreach (char letter in input)
            {
                if ((int)letter < maxAsciiValue)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsDoubleQuoted(string input)
        {
            const byte minimumLength = 2;
            return input.Length >= minimumLength && input[0] == '"' && input[input.Length - 1] == '"';
        }

        private static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }
    }
}
