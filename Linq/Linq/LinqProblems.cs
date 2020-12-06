using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    public static class LinqProblems
    {
        public static int VowelsNumber(this string word)
        {
            if (word == null)
            {
                throw new ArgumentNullException(nameof(word));
            }

            return word.ToCharArray().Count(x => "aeiouAEIOU".Contains(x));
        }
    }
}
