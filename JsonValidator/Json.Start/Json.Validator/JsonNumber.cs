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
            // Fractional number must not end with a dot
            if (input[input.Length - 1] == '.')
            {
                return false;
            }

            int dotPosition = input.IndexOf('.');
            string firstFractionalPart = "";
            string secondFractionalPart = input.Substring(dotPosition + 1);

            // Fractional part must begin with a digit
            if (!char.IsDigit(secondFractionalPart[0]))
            {
                return false;
            }

            if (!FractionalNumberIsSmallerThanOne(input))
            {
                firstFractionalPart = input.Substring(0, dotPosition);
                return NumberStartIsValid(firstFractionalPart)
                    && HasOnlyDigits(firstFractionalPart.Substring(1))
                    && HasOnlyAllowedCharacters(secondFractionalPart);
            }

            return HasOnlyAllowedCharacters(secondFractionalPart);
        }

        private static bool HasOnlyDigits(string number)
        {
            for (int i = 0; i < number.Length; i++)
            {
                if (!char.IsDigit(number[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool FractionalNumberIsSmallerThanOne(string input)
        {
            return input[0] == '.' || (input[0] == '0' && input[1] == '.');
        }

        private static bool NumberStartIsValid(string input)
        {
            return (IsNonZeroDigit(input)
                || IsNegative(input))
                && !IsNegativeAndHasTwoZeros(input);
        }

        private static bool IsNegativeAndHasTwoZeros(string input)
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

        private static bool IsNonZeroDigit(string input)
        {
            return input[0] >= '1' && input[0] <= '9';
        }

        private static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        private static bool HasOnlyAllowedCharacters(string input)
        {
            int index = 1;
            FindExponentSymbolIndex(input, ref index);

            // Number has only digits
            if (index == input.Length)
            {
                return true;
            }

            // Exponent symbol is at the end of the number
            if (index + 1 >= input.Length || index == -1)
            {
                return false;
            }

            return ExponentIsValid(input, index + 1);
        }

        private static void FindExponentSymbolIndex(string input, ref int index)
        {
            while (index < input.Length)
            {
                if (IsExponentSymbol(input[index]))
                {
                    break;
                }

                if (!char.IsDigit(input[index]))
                {
                    index = -1;
                    break;
                }

                index++;
            }
        }

        private static bool ExponentIsValid(string input, int exponentStartPosition)
        {
            if (IsPlusOrMinusOperator(input[exponentStartPosition]))
            {
                exponentStartPosition++;

                // Operator is at the end of the number
                if (exponentStartPosition >= input.Length)
                {
                    return false;
                }
            }

            // The rest of the exponent must be composed of digits
            return HasOnlyDigits(input.Substring(exponentStartPosition));
        }

        private static bool IsPlusOrMinusOperator(char character)
        {
            return character == '+' || character == '-';
        }

        private static bool IsExponentSymbol(char character)
        {
            return character == 'e' || character == 'E';
        }
    }
}
