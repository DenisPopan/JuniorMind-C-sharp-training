using System;
using System.Collections.Generic;
using System.Drawing;

namespace DiagramsProjectV2
{
    public class Flowchart
    {
        public Flowchart(string[] commands)
        {
            AddFlowchartElements(commands);
        }

        public List<Node> Nodes { get; } = new List<Node>();

        public List<Edge> Edges { get; } = new List<Edge>();

        public void AddFlowchartElements(string[] commands)
        {
            ProjectUtils.EnsureIsNotNull(commands, nameof(commands));

            string[] nodesText;
            Node firstNode;
            Node secondNode;

            for (int i = 1; i < commands.Length; i++)
            {
                nodesText = commands[i].Split(" --- ");
                firstNode = Nodes.Find(x => x.Text.Equals(nodesText[0]));
                if (firstNode == null)
                {
                    firstNode = AddNode(nodesText[0]);
                    secondNode = AddNode(nodesText[1]);

                    if (secondNode.Level == 1 || secondNode.Level == 0)
                    {
                        secondNode.Parent = firstNode;
                        secondNode.Level = 2;
                        firstNode.AddChild(secondNode);
                    }

                    firstNode.Level = secondNode.Level - 1;
                    AddEdge(firstNode, secondNode);
                }
            }
        }

        Node AddNode(string text)
        {
            var node = Nodes.Find(x => x.Text.Equals(text));
            if (node == null)
            {
                Nodes.Add(new Node(Nodes.Count + 1, text));
                node = Nodes[Nodes.Count - 1];
            }

            return node;
        }

        void AddEdge(Node firstNode, Node secondNode)
        {
            Edges.Add(new Edge(firstNode, secondNode));
        }
    }
}
