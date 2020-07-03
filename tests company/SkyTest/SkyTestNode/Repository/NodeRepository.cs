using SkyTestNode.Entity;

namespace SkyTestNode.Repository
{
    //note: If a real project, I`ll store this project in a "Infra" where could be connection with DB and other stuff
    //and also implement a test with Moq4 and Bogus to generate a better test with fake repositories, but for now...
    public class NodeRepository : INodeRepository
    {        
        public Node GetNodeRoot()
        {            
            //left nodes
            Node node4 = new Node(4, null, null);
            Node node12 = new Node(12, null, null);
            Node node18 = new Node(18, null, null);
            Node node24 = new Node(24, null, null);           

            Node node10 = new Node(10, node12, node4);
            Node node22 = new Node(22, node24, node18);
            Node node15 = new Node(15, node22, node10);

            //right nodes
            Node node31 = new Node(31, null, null);
            Node node44 = new Node(44, null, null);
            Node node66 = new Node(66, null, null);
            Node node90 = new Node(90, null, null);

            Node node35 = new Node(35, node44, node31);
            Node node70 = new Node(70, node90, node66);
            Node node50 = new Node(50, node70, node35);

            //root (i just need the father, once everyone is inside it)
            Node node25 = new Node(25, node50, node15);
            return node25;
        }
    }
}
