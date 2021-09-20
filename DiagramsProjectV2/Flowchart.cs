using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DiagramsProjectV2
{
    public class Flowchart
    {
        public Flowchart(string[] commands)
        {
            ProjectUtils.EnsureIsNotNull(commands, nameof(commands));
            AddFlowchartElements(commands);
        }

        public List<Node> Nodes { get; } = new List<Node>();

        public List<Edge> Edges { get; } = new List<Edge>();

        public void Draw(string location)
        {
            Canva.InitialiseDrawing();
            float startX = 50;
            float startY = 50;
            FindChildrenWidth();
            DrawGroup(ref startX, startY, Nodes.Where(x => x.Level == 1).OrderBy(x => x.Level));
            startY += 200;

            foreach (var levelGroup in Nodes.OrderBy(x => x.Level).GroupBy(x => x.Level))
            {
                foreach (var node in levelGroup)
                {
                    DrawGroup(node.Rectangle.Right - node.Width / 2 - node.ChildrenWidth / 2, startY, node.GetChildren());
                }

                startY += 200;
            }

            DrawEdges();

            Canva.SaveDrawing(location);
        }

        private void DrawEdges()
        {
            foreach (var edge in Edges)
            {
                Program.DrawLink(edge.FirstNode.Rectangle, edge.SecondNode.Rectangle);
            }
        }

        private void FindChildrenWidth()
        {
            foreach (var node in Nodes.OrderByDescending(x => x.Level))
            {
                node.ChildrenWidth = node.GetChildrenCount() == 0 ? node.Width : CalculateChildrenWidth(node.GetChildren());
            }
        }

        private float CalculateChildrenWidth(List<Node> children)
        {
            float width = -100;
            foreach (var child in children)
            {
                width = width + child.ChildrenWidth + 100;
            }

            return width;
        }

        private void DrawGroup(ref float startX, float startY, IOrderedEnumerable<Node> groupedByLevel)
        {
            foreach (var node in groupedByLevel)
            {
                node.Rectangle = new RectangleF(startX + node.ChildrenWidth / 2, startY, node.Width, node.Height);
                Program.DrawSimpleRectangle(node.Text, node.Rectangle);
                startX = startX + node.ChildrenWidth + 100;
            }
        }

        private void DrawGroup(float startX, float startY, List<Node> children)
        {
            foreach (var node in children)
            {
                node.Rectangle = new RectangleF(node.GetChildrenCount() < 2 ? startX : startX + node.ChildrenWidth / 2, startY, node.Width, node.Height);
                Program.DrawSimpleRectangle(node.Text, node.Rectangle);
                startX = startX + node.ChildrenWidth + 100;
            }
        }

        void AddFlowchartElements(string[] commands)
        {
            string[] nodesText;
            Node firstNode;
            Node secondNode;

            for (int i = 1; i < commands.Length; i++)
            {
                nodesText = commands[i].Split(" --- ");
                firstNode = AddNode(nodesText[0]);
                secondNode = AddNode(nodesText[1]);
                if (firstNode.Level == 0)
                {
                    if (secondNode.Level == 1 || secondNode.Level == 0)
                    {
                        secondNode.Parent = firstNode;
                        secondNode.Level = 2;
                        firstNode.AddChild(secondNode);
                    }

                    firstNode.Level = secondNode.Level - 1;
                }
                else
                {
                    secondNode.Level = firstNode.Level + 1;
                    secondNode.Parent = firstNode;
                    firstNode.AddChild(secondNode);
                }

                AddEdge(firstNode, secondNode);
                secondNode.AddUpperEdge(firstNode.Id);
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