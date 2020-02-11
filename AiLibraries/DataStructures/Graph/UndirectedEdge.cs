namespace AiLibraries.DataStructures.Graph
{
    public class UndirectedEdge : Edge
    {
        public UndirectedEdge(Node node1, Node node2) : base(node1, node2)
        {
        }

        public UndirectedEdge(Node node1, Node node2, int cost)
            : base(node1, node2, cost)
        {
        }
    }
}