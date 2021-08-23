namespace DiagramsProjectV2
{
    public class Edge
    {
        public Edge(Node firstNode, Node secondNode)
        {
            FirstNode = firstNode;
            SecondNode = secondNode;
        }

        public Node FirstNode { get; }

        public Node SecondNode { get; }
    }
}
