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
            if (input.Length == 1)
            {
                return char.IsDigit(input[0]);
            }

            return NumberIsValid(input);
        }

        private static bool NumberIsValid(string input)
        {
            if (NumberIsFractional(input))
            {
                return FractionalNumberIsValid(input);
            }

            return NonFractionalNumberIsValid(input);
        }

        private static bool NumberIsFractional(string input)
        {
            return input.IndexOf('.') != -1;
        }

        private static bool NonFractionalNumberIsValid(string input)
        {
            return NumberStartIsValid(input)
               && HasOnlyAllowedCharacters(input);
        }

        private static bool FractionalNumberIsValid(string input)
        {
            if (input.Length <= 1 || input[input.Length - 1] == '.')
            {
                return false;
            }

            int dotPosition = input.IndexOf('.');
            string firstFractionalPart = "";
            string secondFractionalPart = input.Substring(dotPosition + 1);
            if (!FractionalNumberIsSmallerThanOne(input))
            {
                firstFractionalPart = input.Substring(0, dotPosition);
                return NumberStartIsValid(firstFractionalPart) && HasOnlyAllowedCharacters(secondFractionalPart);
            }

            return HasOnlyAllowedCharacters(secondFractionalPart);
        }

        private static bool FractionalNumberIsSmallerThanOne(string input)
        {
            return input[0] == '.' || (input[0] == '0' && input[1] == '.');
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
            return input[0] == '-' && input[1] == '0' && input[thirdPosition] == '0';
        }

        private static bool IsNegative(string input)
        {
            return input[0] == '-' && char.IsDigit(input[1]);
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
            int index = 1;
            while (index < input.Length)
            {
                if (IsExponentSymbol(input[index]))
                {
                    break;
                }

                if (!char.IsDigit(input[index]))
                {
                    return false;
                }

                index++;
            }

            if (index == input.Length)
            {
                return true;
            }

            if (index + 1 >= input.Length)
            {
                return false;
            }

            return ExponentIsValid(input, index + 1);
        }

        private static bool ExponentIsValid(string input, int exponentStartPosition)
        {
            char[] validCharacters = { '+', '-' };
            if (Array.IndexOf(validCharacters, input[exponentStartPosition]) != -1)
            {
                exponentStartPosition++;

                if (exponentStartPosition >= input.Length)
                {
                    return false;
                }
            }

            for (int i = exponentStartPosition; i < input.Length; i++)
            {
                if (!char.IsDigit(input[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsExponentSymbol(char character)
        {
            return character == 'e' || character == 'E';
        }
    }
}
