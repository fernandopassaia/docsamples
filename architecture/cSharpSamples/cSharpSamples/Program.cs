using System;
using System.Collections.Generic;

namespace cSharpSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var dictionary = new Dictionary<string, int>();
            dictionary.Add("cat", 1);
            dictionary.Add("dog", 2);
            dictionary.Add("passarinho", 3);
            dictionary.Add("peixe", 4);
            // The dictionary has 4 pairs.
            Console.WriteLine("DICTIONARY 1: " + dictionary.Count);

            Console.WriteLine(dictionary["dog"]); //will print dog
            Console.WriteLine(dictionary["pass"]); //will print 3

            Console.ReadKey();
        }
    }
}
