using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            //This is Part of the TEST for LogMeIn made by Fernando Passaia
            //first i will create the TEMP-DATA witch is a object of my class with values
            var tree = new TreeNode("Root")
               {
                   new TreeNode("First Tree")
                       {
                           new TreeNode("FirstChild"),
                           new TreeNode("SecondChild"),
                           new TreeNode("ThirdChild"),
                       },
                   new TreeNode("Second Tree")
                       {
                           new TreeNode("Child1"),
                           new TreeNode("Child2"),
                           new TreeNode("Child3"),                           
                       }
               };

            
            foreach(TreeNode item in tree) //first i enter on my principal Tree and print it ID (First Tree, Second Tree)
            {
                Console.WriteLine(item.ID);
                List<TreeNode> childs = item.ToList();
                for (int i = 0; i < childs.Count; i++) //i'm using "for" instead of ForEach to show second option coding...
                {
                    Console.WriteLine(childs[i].ID);
                }
                Console.WriteLine(); //just to a blank (or better, black) line
            }
            Console.ReadKey();
        }
    }
}
