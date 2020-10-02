using System;
using Json;

namespace JsonValidatorConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = System.IO.File.ReadAllText(args[0]);
            Console.WriteLine(new Value().Match(text).Success());
        }
    }
}
