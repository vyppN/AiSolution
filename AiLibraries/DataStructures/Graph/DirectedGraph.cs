using System;

namespace AiLibraries.DataStructures.Graph
{
    public class DirectedGraph : Graph
    {
        public override void AddEdge(Node node1, Node node2, int cost)
        {
            ProtectedEdges.Add(new DirectedEdge(node1, node2, cost));
        }

        public override void AddEdge(Node node1, Node node2)
        {
            ProtectedEdges.Add(new UndirectedEdge(node1, node2));
        }

        public override Node[] AroundNodes(Node center)
        {
            throw new NotImplementedException();
        }
    }
}