using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DiagramsProjectV2
{
    public class Flowchart
    {
        float currentLevelHeightEndPoint;
        int levels;

        public Flowchart(string[] commands)
        {
            ProjectUtils.EnsureIsNotNull(commands, nameof(commands));
            AddFlowchartElements(commands);
            currentLevelHeightEndPoint = 0;
        }

        public List<Node> Nodes { get; private set; } = new List<Node>();

        public List<Edge> Edges { get; } = new List<Edge>();

        public void MoveNodeTo(Node node, int index)
        {
            var item = node;
            Nodes.Remove(node);
            Nodes.Insert(index, item);
        }

        public void Draw(string location)
        {
            float startY = 50;

            FixNodesOrder();

            TreatSpecialCases();

            FindChildrenWidth();

            SetNodesCoordinates(50, startY, Nodes.Where(x => x.Level == 1));
            DrawGroup(Nodes.Where(x => x.Level == 1));
            startY = currentLevelHeightEndPoint + 130;

            DrawChildren(startY);

            DrawEdges();

            Canva.SaveDrawing(location);
        }

        private void TreatSpecialCases()
        {
            int leftPillarPosition;
            int rightPillarPosition;

            foreach (var groupedEdges in Edges.GroupBy(x => x.SecondNode))
            {
                if (NodeHasMoreThanOneUpperEdge(groupedEdges))
                {
                    leftPillarPosition = Canva.Bitmap.Width + 1;
                    rightPillarPosition = -1;
                    FindPillarsPosition(ref leftPillarPosition, ref rightPillarPosition, groupedEdges.ToList());
                    groupedEdges.Key.Parent = Nodes[(leftPillarPosition + rightPillarPosition) / 2];
                }
            }
        }

        private bool NodeHasMoreThanOneUpperEdge(IEnumerable<Edge> upperEdges)
        {
            return upperEdges.Count() > 1;
        }

        private void FindPillarsPosition(ref int leftPillarPosition, ref int rightPillarPosition, List<Edge> upperEdges)
        {
            foreach (var upperEdge in upperEdges)
            {
                var firstNodePosition = upperEdge.FirstNode.ListPosition;
                if (firstNodePosition < leftPillarPosition)
                {
                    leftPillarPosition = firstNodePosition;
                }

                if (firstNodePosition > rightPillarPosition)
                {
                    rightPillarPosition = firstNodePosition;
                }
            }
        }

        private void FixNodesOrder()
        {
            List<Node> tempList = new List<Node>();
            tempList.AddRange(Nodes.Where(x => x.Level == 1));
            for (int i = 1; i < levels; i++)
            {
                foreach (var node in tempList.Where(x => x.Level == i).ToList())
                {
                    tempList.AddRange(node.GetChildren());
                }
            }

            Nodes = new List<Node>(tempList);
        }

        private void FindChildrenWidth()
        {
            foreach (var node in Nodes.OrderByDescending(x => x.Level))
            {
                var children = Nodes.Where(x => x.Level > 1 && x.Parent.Equals(node));
                node.ChildrenWidth = CalculateChildrenWidth(children);
            }
        }

        private float CalculateChildrenWidth(IEnumerable<Node> children)
        {
            if (!children.Any())
            {
                return 0;
            }

            float width = -100;
            foreach (var child in children)
            {
                width = width + Math.Max(child.Width, child.ChildrenWidth) + 100;
            }

            return width;
        }

        private void SetNodesCoordinates(float startX, float startY, IEnumerable<Node> children)
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
            }
        }

        private void DrawChildren(float startY)
        {
            foreach (var levelGroup in Nodes.Where(x => x.Level > 1).OrderBy(x => x.Level).GroupBy(x => x.Level))
            {
                foreach (var groupedByParent in levelGroup.GroupBy(x => x.Parent))
                {
                    SetNodesCoordinates(groupedByParent.Key.Rectangle.Right - groupedByParent.Key.Width / 2 - groupedByParent.Key.ChildrenWidth / 2, startY, groupedByParent);
                    DrawGroup(groupedByParent);
                }

                startY = currentLevelHeightEndPoint + 130;
            }
        }

        private void DrawGroup(IEnumerable<Node> children)
        {
            foreach (var node in children)
            {
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
            Canva.InitialiseDrawing();
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
                        firstNode.Level = 1;
                        SetParentChildRelationship(firstNode, secondNode);
                    }
                }
                else
                {
                    if (secondNode.Level == 0)
                    {
                        SetParentChildRelationship(firstNode, secondNode);
                    }

                    if (firstNode.Level >= secondNode.Level)
                    {
                        SetParentChildRelationship(firstNode, secondNode);
                    }
                }

                UpdateLevelsNumber(secondNode);

                AddEdge(firstNode, secondNode);
            }
        }

        private void UpdateLevelsNumber(Node secondNode)
        {
            if (levels >= secondNode.Level)
            {
                return;
            }

            levels = secondNode.Level;
        }

        void SetParentChildRelationship(Node firstNode, Node secondNode)
        {
            secondNode.Parent = firstNode;
            secondNode.Level = firstNode.Level + 1;
        }

        Node AddNode(string text)
        {
            var node = Nodes.Find(x => x.Text.Equals(text));
            if (node == null)
            {
                Nodes.Add(new Node(text, text, this));
                node = Nodes[^1];
            }

            return node;
        }

        void AddEdge(Node firstNode, Node secondNode)
        {
            Edges.Add(new Edge(firstNode, secondNode));
        }
    }
}