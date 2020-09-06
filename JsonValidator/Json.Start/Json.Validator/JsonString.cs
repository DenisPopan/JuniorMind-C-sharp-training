using System;

namespace Json
{
    public static class JsonString
    {
        public static bool IsJsonString(string input)
        {
            return HasContent(input)
                && ContentIsValid(input);
        }

        private static bool ContentIsValid(string input)
        {
            return IsDoubleQuoted(input)
                && !ContainsControlCharacters(input)
                && EscapedCharactersAreValid(input);
        }

        private static bool EscapedCharactersAreValid(string input)
        {
            int lastNonQuotesChar = input.Length - 2;
            for (int i = 0; i < input.Length - 1; i++)
            {
                if (input[i] == '\\' && !NextCharacterCanBeEscaped(input, i + 1))
                {
                    return false;
                }
            }

            return input[lastNonQuotesChar] != '\\';
        }

        private static bool NextCharacterCanBeEscaped(string input, int charPosition)
        {
            char[] validChars = { '/', 'b', 'f', 'n', 'r', 't', 'u', '"', ' ', '\\' };
            if (Array.IndexOf(validChars, input[charPosition]) == -1)
            {
                return false;
            }

            return (input[charPosition] != 'u') || UnicodeCodeIsCorrect(input, charPosition + 1);
        }

        private static bool UnicodeCodeIsCorrect(string input, int codeStartPosition)
        {
            int codeEndPosition = codeStartPosition + 3;
            if (codeEndPosition >= input.Length - 1)
            {
                return false;
            }

            for (int i = codeStartPosition; i <= codeEndPosition; i++)
            {
                char currentLetter = input[i];
                if (!(IsDigit(currentLetter) || IsLowerCaseChar(currentLetter) || IsUpperCaseChar(currentLetter)))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsLowerCaseChar(char currentLetter)
        {
            return currentLetter >= 'a' && currentLetter <= 'f';
        }

        private static bool IsUpperCaseChar(char currentLetter)
        {
            return currentLetter >= 'A' && currentLetter <= 'F';
        }

        private static bool IsDigit(char currentLetter)
        {
            return currentLetter >= '0' && currentLetter <= '9';
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