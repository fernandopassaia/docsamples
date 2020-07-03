using System;
using System.Collections.Generic;
using System.Linq;

namespace SkyTestNode.Helpers
{
    public class ArrayPrinterHelper
    {
        public static void Print(IList<int> subItems)
        {
            string consoleOutput = "";
            subItems = subItems.Reverse().ToArray();
            foreach (var item in subItems)
            {
                consoleOutput += item + ", ";
            }
            Console.WriteLine(consoleOutput);
        }
    }
}
