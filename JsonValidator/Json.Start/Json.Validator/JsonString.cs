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
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '\\' && !NextCharacterIsCorrect(input, i + 1) || input[lastNonQuotesChar] == '\\')
                {
                    return false;
                }
            }

            return true;
        }

        private static bool NextCharacterIsCorrect(string input, int charPosition)
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
                if (!(LetterIsDigit(currentLetter) || LetterIsLowerCaseChar(currentLetter) || LetterIsUpperCaseChar(currentLetter)))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool LetterIsLowerCaseChar(char currentLetter)
        {
            const byte lowerCaseLettersAsciiCodeStart = 97;
            const byte lowerCaseLettersAsciiCodeStop = 102;
            return (int)currentLetter >= lowerCaseLettersAsciiCodeStart && (int)currentLetter <= lowerCaseLettersAsciiCodeStop;
        }

        private static bool LetterIsUpperCaseChar(char currentLetter)
        {
            const byte upperCaseLettersAsciiCodeStart = 65;
            const byte upperCaseLettersAsciiCodeStop = 70;
            return (int)currentLetter >= upperCaseLettersAsciiCodeStart && (int)currentLetter <= upperCaseLettersAsciiCodeStop;
        }

        private static bool LetterIsDigit(char currentLetter)
        {
            const byte digitsAsciiCodeStart = 48;
            const byte digitsAsciiCodeStop = 57;
            return (int)currentLetter >= digitsAsciiCodeStart && (int)currentLetter <= digitsAsciiCodeStop;
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