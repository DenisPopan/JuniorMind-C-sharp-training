using System;
using Json;

namespace JsonValidatorConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            if (string.IsNullOrEmpty(args[0]))
            {
                Console.WriteLine("Json type data input required.");
                Console.Read();
                return;
            }

            string text = System.IO.File.ReadAllText(args[0]);
            var checkedJson = new Value().Match(text);

            Console.WriteLine("Input Json data is " + (string.IsNullOrEmpty(checkedJson.RemainingText()) && checkedJson.Success() ? "correct!" : "incorrect!"));
            Console.Read();
        }
    }
}
