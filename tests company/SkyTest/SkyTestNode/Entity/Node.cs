
namespace SkyTestNode.Entity
{
    public class Node
    {        
        public int Id { get; private set; }
        public Node Node1 { get; private set; }
        public Node Node2 { get; private set; }

        // note: i`ve kept the constructor because one of the good patterns says just a entity can change
        // itself. If setters are public, everyone can change the entity, braking one of the concepts
        public Node(int id, Node node1, Node node2)
        {
            Id = id;
            Node1 = node1;
            Node2 = node2;
        }
    }
}
