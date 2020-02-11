namespace AiLibraries.DataStructures.Graph
{
    public abstract class Edge
    {
        protected Edge(Node node1, Node node2)
        {
            Nodes = new Node[2];
            Connect(node1, node2);
            Cost = 0;
        }

        protected Edge(Node node1, Node node2, int cost)
        {
            Nodes = new Node[2];
            Connect(node1, node2);
            Cost = cost;
        }

        public int Cost { get; set; }
        public Node[] Nodes { get; set; }

        public void Connect(Node node1, Node node2)
        {
            Nodes[0] = node1;
            Nodes[1] = node2;
        }
    }
}