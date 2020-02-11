using System.Collections.Generic;

namespace AiLibraries.DataStructures.Graph
{
    public class UndirectedGraph : Graph
    {
        public override void AddEdge(Node node1, Node node2, int cost)
        {
            ProtectedEdges.Add(new UndirectedEdge(node1, node2, cost));
        }

        public override void AddEdge(Node node1, Node node2)
        {
            ProtectedEdges.Add(new UndirectedEdge(node1, node2));
        }

        public override Node[] AroundNodes(Node center)
        {
            var around = new List<Node>();
            List<Edge> nearEdge = ProtectedEdges.FindAll(x => x.Nodes[0] == center || x.Nodes[1] == center);
            foreach (Edge edge in nearEdge)
            {
                around.Add((edge.Nodes[0] == center) ? edge.Nodes[1] : edge.Nodes[0]);
            }
            return around.ToArray();
        }
    }
}