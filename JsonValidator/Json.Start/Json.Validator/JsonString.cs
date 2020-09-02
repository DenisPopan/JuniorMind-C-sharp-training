using System;

namespace Json
{
    public static class JsonString
    {
        public static bool IsJsonString(string input)
        {
            return HasContent(input)
                && IsDoubleQuoted(input);
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
