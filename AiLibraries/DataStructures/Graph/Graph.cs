using System.Collections.Generic;

namespace AiLibraries.DataStructures.Graph
{
    public abstract class Graph
    {
        protected List<Edge> ProtectedEdges = new List<Edge>();
        protected List<Node> ProtectedNodes = new List<Node>();

        public Node[] Nodes
        {
            get { return ProtectedNodes.ToArray(); }
        }

        public Edge[] Edges
        {
            get { return ProtectedEdges.ToArray(); }
        }

        public void AddNode(string nodeName)
        {
            ProtectedNodes.Add(new Node(nodeName));
        }

        public void AddNode(Node node)
        {
            if (!ProtectedNodes.Exists(x => x.Name == node.Name))
                ProtectedNodes.Add(node);
            else
                throw new DuplicateNameException();
        }

        public Node FindNodeByName(string nodeName)
        {
            return ProtectedNodes.Find(x => x.Name == nodeName);
        }

        public Node[] BreadthFirst(Node startNode)
        {
            var passed = new List<Node>();
            var q = new Queue<Node>();
            q.Enqueue(startNode);
            while (q.Count != 0)
            {
                Node thisNode = q.Dequeue();
                passed.Add(thisNode);
                var temp = new List<Node>();
                temp.AddRange(AroundNodes(thisNode));
                temp.RemoveAll(x => passed.Contains(x) || q.Contains(x));
                foreach (Node x in temp) q.Enqueue(x);
            }
            return passed.ToArray();
        }

        public Node[] DepthFirst(Node startNode)
        {
            var passed = new List<Node>();
            var st = new Stack<Node>();
            st.Push(startNode);
            while (st.Count != 0)
            {
                Node thisNode = st.Pop();
                passed.Add(thisNode);
                var temp = new List<Node>();
                temp.AddRange(AroundNodes(thisNode));
                temp.RemoveAll(x => passed.Contains(x) || st.Contains(x));
                foreach (Node x in temp) st.Push(x);
            }
            return passed.ToArray();
        }

        public abstract void AddEdge(Node node1, Node node2, int cost);
        public abstract void AddEdge(Node node1, Node node2);
        public abstract Node[] AroundNodes(Node center);
    }
}