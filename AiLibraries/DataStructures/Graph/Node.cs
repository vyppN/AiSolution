namespace AiLibraries.DataStructures.Graph
{
    public class Node
    {
        public Node()
        {
            Name = "A";
        }

        public Node(string nodeName)
        {
            Name = nodeName;
        }

        public string Name { get; set; }
    }
}