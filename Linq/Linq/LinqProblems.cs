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
