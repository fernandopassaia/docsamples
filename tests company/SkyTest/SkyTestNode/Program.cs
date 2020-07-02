using SkyTestNode.Entity;
using System;

namespace SkyTestNode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Node node4 = new Node();
            node4.Id = 4;

            Node node12 = new Node();
            node12.Id = 12;

            Node node10 = new Node();
            node10.Id = 12;
            node10.Node1 = node4;
            node10.Node2 = node12;

            Node node18 = new Node();
            node18.Id = 18;

            Node node24 = new Node();
            node12.Id = 24;

            Node node22 = new Node();
            node22.Id = 12;
            node22.Node1 = node4;
            node22.Node2 = node12;
        }
    }
}
