using System;
using IntegersArray;

namespace ForeachTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var objectArray = new ObjectArray { 1, 6, 12, "hey" };
            foreach (var element in objectArray)
            {
                Console.Write("it works! ");
            }
        }
    }
}
