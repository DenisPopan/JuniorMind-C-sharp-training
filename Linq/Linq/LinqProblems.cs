using System;
using System.Linq;

namespace Linq
{
    public static class LinqProblems
    {
        public static int VowelsNumber(this string text)
        {
            EnsureIsNotNull(text, nameof(text));

            return text.ToCharArray().Count(x => "aeiouAEIOU".Contains(x));
        }

        public static int ConsonantsNumber(this string text)
        {
            EnsureIsNotNull(text, nameof(text));

            return text.Length - VowelsNumber(text);
        }

        public static char FirstUnique(this string text)
        {
            EnsureIsNotNull(text, nameof(text));
            foreach (var group in text.ToCharArray().GroupBy(x => x))
            {
                if (group.Count() == 1)
                {
                    return group.Key;
                }
            }

            return '-';
        }

        public static int ToInt(this string text)
        {
            EnsureIsNotNull(text, nameof(text));

            int number = 0;
            int sign = 1;

            if (text[0] == '-')
            {
                sign = -1;
            }

            foreach (var digit in text.ToCharArray().Select(x => char.GetNumericValue(x)).Where(x => x > -1))
            {
                number = number * 10 + (int)digit;
            }

            return number * sign;
        }

        public static char MaxOccurrence(this string text)
        {
            EnsureIsNotNull(text, nameof(text));
            int maxOccurrence = 0;
            char maxOccurrenceChar = '-';
            foreach (var group in text.ToCharArray().GroupBy(x => x))
            {
                if (group.Count() > maxOccurrence)
                {
                    maxOccurrence = group.Count();
                    maxOccurrenceChar = group.Key;
                }
            }

            return maxOccurrenceChar;
        }

        static void EnsureIsNotNull<T>(T source, string name)
        {
            if (source != null)
            {
                return;
            }

            throw new ArgumentNullException(name);
        }
    }
}
