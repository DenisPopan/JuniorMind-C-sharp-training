using System;
using IntegersArray;

namespace ForeachTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var objectArray = new ObjectArray { 1, 2, 3 };
            foreach (var element in objectArray)
            {
                Console.Write("it works! ");
            }
        }
    }
}
