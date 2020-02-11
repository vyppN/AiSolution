﻿namespace AiLibraries.DataStructures.Graph
{
    public class DirectedEdge : Edge
    {
        public DirectedEdge(Node node1, Node node2) : base(node1, node2)
        {
        }

        public DirectedEdge(Node node1, Node node2, int cost) : base(node1, node2, cost)
        {
        }
    }
}