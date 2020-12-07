using System;
using System.Linq;

namespace Linq
{
    public static class LinqProblems
    {
        public static int VowelsNumber(this string word)
        {
            EnsureIsNotNull(word, nameof(word));

            return word.ToCharArray().Count(x => "aeiouAEIOU".Contains(x));
        }

        public static int ConsonantsNumber(this string word)
        {
            EnsureIsNotNull(word, nameof(word));

            return word.Length - VowelsNumber(word);
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
