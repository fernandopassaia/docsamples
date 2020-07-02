using SkyTestNode.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkyTestNode.Repository
{
    //note: If a real project, I`ll store this project in a "Infra" where could be connection with DB and other stuff
    //and also implement a test with Moq4 and Bogus to generate a better test with fake repositories, but for now...
    public class NodeRepository : INodeRepository
    {
        public List<Node> getDataNodes()
        {
            List<Node> nodes = new List<Node>();
            //here I`ll generate the data in the structure of sample
            nodes.Add(new Node(15, 50));

            //i`m drawing it guys

            return nodes;
        }       
        
    }
}
