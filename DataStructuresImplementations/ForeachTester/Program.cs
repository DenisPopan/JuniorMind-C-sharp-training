﻿using System;
using IntegersArray;

namespace ForeachTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var objectArray = new List<int> { 1, 2, 3, 4, 5};
            foreach (var element in objectArray)
            {
                Console.Write("it works! ");
            }
        }
    }
}
