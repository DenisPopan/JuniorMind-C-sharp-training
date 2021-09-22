using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DiagramsProjectV2
{
    public class Flowchart
    {
        float currentLevelHeightEndPoint;

        public Flowchart(string[] commands)
        {
            ProjectUtils.EnsureIsNotNull(commands, nameof(commands));
            AddFlowchartElements(commands);
            currentLevelHeightEndPoint = 0;
        }

        public List<Node> Nodes { get; } = new List<Node>();

        public List<Edge> Edges { get; } = new List<Edge>();

        public void Draw(string location)
        {
            float startY = 50;
            Canva.InitialiseDrawing();

            FindChildrenWidth();

            DrawGroup(50, startY, Nodes.Where(x => x.Level == 1));
            startY = currentLevelHeightEndPoint + 130;

            DrawChildren(startY);

            DrawEdges();

            Canva.SaveDrawing(location);
        }

        private void FindChildrenWidth()
        {
            foreach (var node in Nodes.OrderByDescending(x => x.Level))
            {
                node.ChildrenWidth = CalculateChildrenWidth(node.GetChildren());
            }
        }

        private float CalculateChildrenWidth(List<Node> children)
        {
            if (children.Count == 0)
            {
                return 0;
            }

            float width = -100;
            foreach (var child in children)
            {
                width = child.GetChildrenCount() == 0 ? width + child.Width + 100 : width + Math.Max(child.Width, child.ChildrenWidth) + 100;
            }

            return width;
        }

        private void DrawChildren(float startY)
        {
            foreach (var levelGroup in Nodes.OrderBy(x => x.Level).GroupBy(x => x.Level))
            {
                foreach (var node in levelGroup)
                {
                    DrawGroup(node.Rectangle.Right - node.Width / 2 - node.ChildrenWidth / 2, startY, node.GetChildren());
                }

                startY = currentLevelHeightEndPoint + 130;
            }
        }

        private void DrawGroup(float startX, float startY, IEnumerable<Node> children)
        {
            foreach (var node in children)
            {
                if (node.Width > node.ChildrenWidth)
                {
                    node.Rectangle = new RectangleF(startX, startY, node.Width, node.Height);
                    startX = node.Rectangle.Right + 100;
                }
                else
                {
                    node.Rectangle = new RectangleF(startX + node.ChildrenWidth / 2 - node.Width / 2, startY, node.Width, node.Height);
                    startX = startX + node.ChildrenWidth + 100;
                }

                if (node.Rectangle.Bottom > currentLevelHeightEndPoint)
                {
                    currentLevelHeightEndPoint = node.Rectangle.Bottom;
                }

                Program.DrawSimpleRectangle(node.Text, node.Rectangle);
            }
        }

        private void DrawEdges()
        {
            foreach (var edge in Edges)
            {
                Program.DrawLink(edge.FirstNode.Rectangle, edge.SecondNode.Rectangle);
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