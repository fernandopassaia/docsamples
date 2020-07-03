using System;
using System.Collections.Generic;
using System.Text;

namespace SkyTestNode.Helpers
{
    public class ArrayPrinterHelper
    {
        public static void Print(IList<int> subItems)
        {
            string consoleOutput = "";
            foreach (var item in subItems)
            {
                consoleOutput += item + ", ";
            }
            Console.WriteLine(consoleOutput);
        }
    }
}
