using System;

namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
        {
            return input.IndexOf('0') != -1;
        }
    }
}
