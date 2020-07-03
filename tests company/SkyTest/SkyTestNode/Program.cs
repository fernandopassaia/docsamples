using SkyTestNode.Helpers;
using SkyTestNode.Repository;
using System;

namespace SkyTestNode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // note: in a real project, I`ll inject over constructor a implementation of INodeRepository using DI
            // but because this is just a sample, I`ll not install and configure DI and other things...
            NodeRepository nodeRepo = new NodeRepository();
            var nodeRoot = nodeRepo.GetNodeRoot(); //this node will represent clearly the part 2 of exercise
            ArrayPrinterHelper.Print(NodeValuesHelper.extractNodeValues(nodeRoot));            
            Console.ReadKey();
        }

        
    }
}
